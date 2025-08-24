using API.Core.JWT;
using Application.Common;
using Application.UseCases.Commands;
using Application.UseCases.Commands.Apartments;
using Application.UseCases.Commands.Bookings;
using Application.UseCases.Commands.Lookup;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Apartment;
using Application.UseCases.Queries.Bookings;
using Application.UseCases.Queries.Chat;
using Application.UseCases.Queries.Lookup;
using Application.UseCases.Queries.Users;
using Implementation.Common;
using Implementation.UseCases;
using Implementation.UseCases.Commands;
using Implementation.UseCases.Commands.Apartments;
using Implementation.UseCases.Commands.Bookings;
using Implementation.UseCases.Commands.Lookup;
using Implementation.UseCases.Commands.Lookup.CityCountry;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries;
using Implementation.UseCases.Queries.Apartments;
using Implementation.UseCases.Queries.Bookings;
using Implementation.UseCases.Queries.Chat;
using Implementation.UseCases.Queries.Lookup;
using Implementation.UseCases.Queries.Users;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json;

namespace API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IGetUseCaseLogsQuery, EfGetUseCaseLogsQuery>();
            services.AddTransient<IGetErrorLogsQuery, EfGetErrorLogsQuery>();
            services.AddTransient<IEmailSender, EmailSender>();


            //User
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IFindUserQuery, EfFindUserQuery>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IChangeProfilePhotoCommand, EfChangeProfilePhotoCommand>();
            services.AddTransient<IConfirmEmailCommand, EfConfirmEmailCommand>();
            services.AddTransient<IResendCodeCommand, EfResendCodeCommand>();

            //Lookup
            services.AddTransient<ICreateApartmentTypeCommand, EfCreateApartmentTypeCommand>();
            services.AddTransient<CreateApartmentTypeValidator>();
            services.AddTransient<ICreateCityCommand, EfCreateCityCommand>();
            services.AddTransient<CreateCityValidator>();
            services.AddTransient<ICreateCountryCommand, EfCreateCountryCommand>();
            services.AddTransient<CreateCountryValidator>();
            services.AddTransient<ICreateFeaturesCommand, EfCreateFeaturesCommand>();
            services.AddTransient<CreateFeaturesValidator>();
            services.AddTransient<ICreatePaymentCommand, EfCreatePaymentCommand>();
            services.AddTransient<CreatePaymentValidator>();
            services.AddTransient<ICreateCityCountryCommand, EfCreateCityCountryCommand>();
            services.AddTransient<CreateCityCountryValidator>();


            services.AddTransient<IGetApartmentTypesQuery, EfGetApartmentTypesQuery>();
            services.AddTransient<IGetCitiesQuery, EfGetCitiesQuery>();
            services.AddTransient<IGetCountriesQuery, EfGetCountriesQuery>();
            services.AddTransient<IGetFeaturesQuery, EfGetFeaturesQuery>();
            services.AddTransient<IGetPaymentsQuery, EfGetPaymentsQuery>();
            services.AddTransient<IGetCitiesByCountryQuery, EFGetCitiesByCountryQuery>();


            //Apartment
            services.AddTransient<ICreateApartmentCommand, EfCreateApartmentCommand>();
            services.AddTransient<CreateApartmentValidator>();
            services.AddTransient<IFindApartmentQuery, EfFindApartmentQuery>();
            services.AddTransient<IGetApartmentsQuery, EfGetApartmentsQuery>();
            services.AddTransient<IDeleteApartmentCommand, EfDeleteApartmentCommand>();
            services.AddTransient<IUpdateApartmentCommand, EfUpdateApartmentCommand>();
            services.AddTransient<IUpdateApartmentImagesCommand, EfUpdateApartmentImagesCommand>();
            services.AddTransient<UpdateApartmentValidator>();
            services.AddTransient<IAddApartmentToFavoriteCommand, EfAddApartmentToFavoriteCommand>();
            services.AddTransient<IGetFavoriteApartments, EfGetFavoriteApartments>();   
            services.AddTransient<IGetRatingsQuery, EfGetRatingsQuery>();   
            services.AddTransient<ICreateRatingCommand, EfCreateRatingCommand>();
            services.AddTransient<CreateRatingValidator>();
            services.AddTransient<IArchiveApartmentCommand, EfArchiveApartmentCommand>();
            services.AddTransient<IGetArchivedApartmentsQuery, EfGetArchivedApartmentsQuery>();
            services.AddTransient<IActivateApartmentCommand, EfActivateApartmentCommand>();

            //Booking
            services.AddTransient<ICreateBookingCommand, EfCreateBookingCommand>();
            services.AddTransient<CreateBookingValidator>();
            services.AddTransient<IUpdateBookingCommand, EfUpdateBookingCommand>();
            services.AddTransient<UpdateBookingValidator>();
            services.AddTransient<ICancelBookingCommand, EfCancelBookingCommand>();
            services.AddTransient<IGetMyBookingsQuery, EfGetMyBookingsQuery>();
            services.AddTransient<IGetMyGuestBookingsQuery, EfGetMyGuestBookingsQuery>();
            services.AddTransient<IFindBookingQuery, EfFindBookingQuery>();

            //Chat
            services.AddScoped<ISendMessageCommand, EfSaveChatCommand>();
            services.AddTransient<IGetChatListQuery, EfGetChatListQuery>();
            services.AddTransient<IGetChatMessagesQuery, EfGetChatMessages>();
            services.AddTransient<IPrepareChatQuery, EfPrepareChatQuery>();



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
                    //OnTokenValidated = context =>
                    //{

                    //    Guid tokenId = context.HttpContext.Request.GetTokenId().Value;

                    //    ITokenStorage storage = services.BuildServiceProvider().GetService<ITokenStorage>();

                    //    if (!storage.Exists(tokenId))
                    //    {
                    //        context.Fail("Invalid token");
                    //    }

                    //    return Task.CompletedTask;

                    //},
                    //OnChallenge = context =>
                    //{
                    //    context.HandleResponse();
                    //    if (!context.Response.HasStarted)
                    //    {
                    //        throw new UnauthorizedAccessException("Authentication Failed.");
                    //    }

                    //    return Task.CompletedTask;
                    //},
                    //OnForbidden = _ =>
                    //{
                    //    throw new UnauthorizedAccessException("You are not authorized to access this resource.");
                    //},
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat-hub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var tmp = connection.User;
            var userId = connection.User?.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            return userId;
        }
    }
}
