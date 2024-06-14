using API.Core.JWT;

namespace API
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public JwtSettings Jwt { get; set; }
    }

}
