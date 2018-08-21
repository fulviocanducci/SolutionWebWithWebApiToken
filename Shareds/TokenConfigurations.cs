namespace Shareds
{
    public sealed class TokenConfigurations
    {
        public TokenConfigurations()
        {

        }

        public TokenConfigurations(string audience, string issuer, int seconds)
        {
            Audience = audience;
            Issuer = issuer;
            Seconds = seconds;
        }

        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
