using API;
using API.Core;
using API.Core.Exceptions;
using API.Core.JWT;
using Application;
using Application.UseCases.Commands.Lookup;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.ApartmentType;
using DataAccess;
using Implementation;
using Implementation.Logging.UseCases;
using Implementation.UseCases;
using Implementation.UseCases.Commands.Lookup;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries.ApartmentType;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var settings = new AppSettings();

builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings.Jwt);



/* TODO: Subota
 
    - Business Logic

 */


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

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
