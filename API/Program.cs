using API;
using API.Core;
using API.Core.Exceptions;
using API.Core.JWT;
using Application;
using DataAccess;
using Implementation;
using Implementation.Logging.UseCases;


var builder = WebApplication.CreateBuilder(args);

var settings = new AppSettings();

builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings.Jwt);


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

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
