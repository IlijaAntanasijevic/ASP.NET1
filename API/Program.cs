using API.Core.Exceptions;
using Application;
using Application.UseCases.Commands.ApartmentType;
using Application.UseCases.Commands.Users;
using DataAccess;
using Implementation;
using Implementation.Logging.UseCases;
using Implementation.UseCases.Commands.ApartmentType;
using Implementation.UseCases.Commands.Users;
using Implementation.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<BookingContext>();
builder.Services.AddTransient<IUseCaseLogger, ConsoleUseCaseLogger>();
builder.Services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();

builder.Services.AddTransient<ICreateApartmentTypeCommand, EfCreateApartmentTypeCommand>();
builder.Services.AddTransient<CreateApartmentTypeValidator>();

builder.Services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
builder.Services.AddTransient<RegisterUserValidator>();



#region Actors
builder.Services.AddTransient<IApplicationActorProvider>(x =>
{
    return new DefaultActorProvider();
});


builder.Services.AddTransient<IApplicationActor>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    if (accessor == null || accessor.HttpContext == null)
    {
        return new UnauthorizedActor();
    }

    return x.GetService<IApplicationActorProvider>().GetActor();
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
