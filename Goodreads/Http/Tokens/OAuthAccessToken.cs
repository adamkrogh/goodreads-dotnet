namespace Goodreads.Http
{
    public class OAuthAccessToken : OAuthToken
    {
        public OAuthAccessToken(string token, string secret)
            : base(token, secret)
        {
        }
    }
}
