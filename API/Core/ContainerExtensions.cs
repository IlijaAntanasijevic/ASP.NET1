using API.Core.JWT;
using Application.UseCases.Commands.Lookup;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.ApartmentType;
using Application.UseCases.Queries.Users;
using Implementation.UseCases;
using Implementation.UseCases.Commands.Lookup;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries.ApartmentType;
using Implementation.UseCases.Queries.Users;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<UseCaseHandler>();
          

            //Apartment Type
            services.AddTransient<ICreateApartmentTypeCommand, EfCreateApartmentTypeCommand>();
            services.AddTransient<CreateApartmentTypeValidator>();
            services.AddTransient<IGetApartmentTypesQuery, EfGetApartmentTypesQuery>();

            //User
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IFindUserQuery, EfFindUserQuery>();
            services.AddTransient<IDeleteUserCommand,EfDeleteUserCommand>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();

            //Lookup
            services.AddTransient<ICreateCityCommand, EfCreateCityCommand>();
            services.AddTransient<CreateCityValidator>();
            services.AddTransient<ICreateCountryCommand, EfCreateCountryCommand>();
            services.AddTransient<CreateCountryValidator>();

        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }


        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddAuthentication(options =>
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

                        ITokenStorage storage = services.BuildServiceProvider().GetService<ITokenStorage>();

                        if (!storage.Exists(tokenId))
                        {
                            context.Fail("Invalid token");
                        }

                        return Task.CompletedTask;

                    }
                };
            });

        }
    }
}
