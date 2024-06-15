using API;
using API.Core;
using API.Core.Exceptions;
using API.Core.JWT;
using Application;
using Application.UseCases.Commands.ApartmentType;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.ApartmentType;
using DataAccess;
using Implementation;
using Implementation.Logging.UseCases;
using Implementation.UseCases;
using Implementation.UseCases.Commands.ApartmentType;
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

// Add services to the container.

/* TODO: Subota
 
    - Fix DbExLogger - Actor (unauth actor)
    - Files
    - Business Logic

 */

// => Last use case: 3

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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

#region JwtToken
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = settings.Jwt.Issuer,
        ValidateIssuer = true,
        ValidAudience = "Any",
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.SecretKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    cfg.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {

            Guid tokenId = context.HttpContext.Request.GetTokenId().Value;

            ITokenStorage storage = builder.Services.BuildServiceProvider().GetService<ITokenStorage>();

            if (!storage.Exists(tokenId))
            {
                context.Fail("Invalid token");
            }

            return Task.CompletedTask;

        }
    };
});

#endregion

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

app.Run();
