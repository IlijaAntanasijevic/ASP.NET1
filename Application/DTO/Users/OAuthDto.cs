using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.Users
{
    public class OAuthDto
    {
        public string Code { get; set; }
    }

    public class GoogleTokenResponseDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }
    }

    public class GoogleUserInfo
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("given_name")]
        public string Name { get; set; }
        [JsonPropertyName("family_name")]
        public string LastName { get; set; }

        [JsonPropertyName("picture")]
        public string Picture { get; set; }
    }
}
