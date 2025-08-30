using App.Domain;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using Implementation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfOAuthRegisterCommand : EfUseCase, IOAuthRegisterCommand
    {
        private readonly OAuthGoogleSettings _settings;
        public EfOAuthRegisterCommand(BookingContext context, OAuthGoogleSettings settings)
            : base(context)
        {
            _settings = settings;
        }

        public int Id => 51;
        public string Name => nameof(EfOAuthRegisterCommand);

        public void Execute(OAuthDto data)
        {
            ExecuteInternal(data).GetAwaiter().GetResult();
        }

        private async Task ExecuteInternal(OAuthDto data)
        {
            if (data == null || string.IsNullOrEmpty(data.Code))
            {
                throw new ValidationException("Missing code");
            }

            var tokenRequest = new Dictionary<string, string>
            {
                {"code", data.Code},
                {"client_id", _settings.ClientId},
                {"client_secret", _settings.ClientSecret},
                {"redirect_uri", _settings.RedirectUri},
                {"grant_type", _settings.GrantType}
            };

            using var http = new HttpClient();
            var response = await http.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(tokenRequest));
            var tokenRespone = await response.Content.ReadAsStringAsync();

            var tokenData = JsonSerializer.Deserialize<GoogleTokenResponseDto>(tokenRespone);

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenData.AccessToken);

            var userInfo = await http.GetStringAsync("https://www.googleapis.com/oauth2/v2/userinfo");

            var googleUser = JsonSerializer.Deserialize<GoogleUserInfo>(userInfo);


            var user = Context.Users.FirstOrDefault(x => x.Email == googleUser.Email);

            if (user != null)
            {
                throw new ValidationException("User already exist");
            }

            User newUser = new User
            {
                Email = googleUser.Email,
                Password = null,
                FirstName = googleUser.Name,
                LastName = googleUser.LastName,
                Phone = "X",
                Avatar = googleUser.Picture,
                IsActive = true,
                IsOAuth = true,
                UseCases = Extensions.AddUserUseCases()
            };

            Context.Add(newUser);
            Context.SaveChanges();

        }
    }
}
