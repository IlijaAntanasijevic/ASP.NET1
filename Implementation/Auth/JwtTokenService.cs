using API.Core.JWT;
using Application.Auth;
using Application.Exceptions;
using DataAccess;
using Implementation.Common;
using Implementation.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Auth
{
    public class JwtTokenService : ITokenService
    {
        private readonly BookingContext _context;
        private readonly JwtSettings _settings;
        private readonly ITokenStorage _storage;

        public JwtTokenService(BookingContext context, JwtSettings settings, ITokenStorage storage)
        {
            _context = context;
            _settings = settings;
            _storage = storage;
        }

        public string GenerateToken(string email, string password)
        {
            var user = _context.Users.Where(x => x.Email == email).Select(x => new
            {
                x.Email,
                x.Password,
                x.FirstName,
                x.LastName,
                x.Id,
                x.IsOAuth,
                x.IsActive,
                UseCaseIds = x.UseCases.Select(x => x.UseCaseId)
            }).FirstOrDefault();

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!user.IsActive && !user.IsOAuth)
            {
                var userNotActive = _context.EmailConfirmations.FirstOrDefault(x => x.UserId == user.Id);
                if (userNotActive != null)
                {
                    throw new PermissionDeniedException("Please activate your account");
                }
            }

            if (!user.IsOAuth && !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException();
            }

            Guid tokenGuid = Guid.NewGuid();

            string tokenId = tokenGuid.ToString();

            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Jti, tokenId, ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer, ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                 new Claim("Email", user.Email),
                 new Claim("FirstName", user.FirstName),
                 new Claim("LastName", user.LastName),
                 new Claim("IsOAuth", user.IsOAuth.ToString()),
                 new Claim("Id", user.Id.ToString()),
                 new Claim("UseCaseIds", JsonConvert.SerializeObject(user.UseCaseIds)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(_settings.Seconds),
                signingCredentials: credentials);

            _storage.Add(tokenGuid);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
