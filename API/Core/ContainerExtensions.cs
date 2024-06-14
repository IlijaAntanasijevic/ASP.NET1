using Application.UseCases.Commands.ApartmentType;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.ApartmentType;
using Implementation.UseCases.Commands.ApartmentType;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries.ApartmentType;
using Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;

namespace API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            //Apartment Type
            services.AddTransient<ICreateApartmentTypeCommand, EfCreateApartmentTypeCommand>();
            services.AddTransient<CreateApartmentTypeValidator>();
            services.AddTransient<IGetApartmentTypesQuery, EfGetApartmentTypesQuery>();

            //User
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<RegisterUserValidator>();

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
    }
}
