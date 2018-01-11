namespace Goodreads.Http
{
    public class OAuthRequestToken : OAuthToken
    {
        public string AuthorizeUrl { get; }

        public OAuthRequestToken(string token, string secret, string authorizeUrl)
            : base(token, secret)
        {
            AuthorizeUrl = authorizeUrl;
        }

        public OAuthRequestToken(string token, string secret)
            : this(token, secret, string.Empty)
        {
        }
    }
}
