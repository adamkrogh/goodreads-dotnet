namespace Goodreads.Http
{
    public class ApiCredentials
    {
        /// <summary>
        ///  The client's Goodreads API key.
        /// </summary>
        public string ApiKey { get; private set; }

        /// <summary>
        /// The client's Goodreads API secret.
        /// </summary>
        public string ApiSecret { get; private set; }

        /// <summary>
        /// The user's OAuth token.
        /// </summary>
        public string OAuthToken { get; private set; }

        /// <summary>
        /// The user's OAuth token secret.
        /// </summary>
        public string OAuthTokenSecret { get; private set; }

        public ApiCredentials(string apiKey, string apiSecret, string oAuthToken, string oAuthTokenSecret)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            OAuthToken = oAuthToken;
            OAuthTokenSecret = oAuthTokenSecret;
        }
    }
}
