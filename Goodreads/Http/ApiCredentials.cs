namespace Goodreads.Http
{
    public class ApiCredentials
    {
        /// <summary>
        ///  The client's Goodreads API key.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// The client's Goodreads API secret.
        /// </summary>
        public string ApiSecret { get; }

        /// <summary>
        /// The user's OAuth token.
        /// </summary>
        public string OAuthToken { get; }

        /// <summary>
        /// The user's OAuth token secret.
        /// </summary>
        public string OAuthTokenSecret { get; }

        public ApiCredentials(string apiKey, string apiSecret, string oAuthToken, string oAuthTokenSecret)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            OAuthToken = oAuthToken;
            OAuthTokenSecret = oAuthTokenSecret;
        }
    }
}
