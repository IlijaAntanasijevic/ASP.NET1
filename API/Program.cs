using API;
using API.Chat;
using API.Core;
using API.Core.Exceptions;
using API.Core.JWT;
using Application;
using Application.UseCases.Commands;
using DataAccess;
using Implementation;
using Implementation.Logging.UseCases;
using Implementation.UseCases.Commands;


var builder = WebApplication.CreateBuilder(args);

var settings = new AppSettings();

builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings.Jwt);
builder.Services.AddSignalR();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();



builder.Services.AddTransient(x => new BookingContext(settings.ConnectionString));
builder.Services.AddTransient<IUseCaseLogger, DbUseCaseLogger>();
builder.Services.AddTransient<IExceptionLogger, DbExceptionLogger>();

builder.Services.AddUseCases();

builder.Services.AddTransient<JwtTokenCreator>();
builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();

builder.Services.AddTransient<IFileUploader, BasicFileUploader>();

builder.Services.AddScoped<ISaveChatCommand, EfSaveChatCommand>();


#region Actors
builder.Services.AddTransient<IApplicationActorProvider>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();

    var request = accessor.HttpContext.Request;

    var authHeader = request.Headers.Authorization.ToString();

    var context = x.GetService<BookingContext>();

    return new JwtApplicationActorProvider(authHeader);
});


builder.Services.AddTransient<IApplicationActor>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    if (accessor.HttpContext == null)
    {
        return new UnauthorizedActor();
    }

    return x.GetService<IApplicationActorProvider>().GetActor();
});
#endregion

builder.Services.AddJwt(settings);

var app = builder.Build();

app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<ChatHub>("chat-hub");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type");
    }
});

app.Run();
