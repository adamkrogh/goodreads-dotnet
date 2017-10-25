namespace Goodreads.Http
{
    public abstract class OAuthToken
    {
        public string Token { get; }

        public string Secret { get; }

        protected OAuthToken(string token, string secret)
        {
            Token = token;
            Secret = secret;
        }
    }
}
