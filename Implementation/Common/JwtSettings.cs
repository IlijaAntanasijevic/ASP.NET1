namespace Implementation.Common
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
