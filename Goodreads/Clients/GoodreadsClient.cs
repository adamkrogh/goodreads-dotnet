using Goodreads.Http;

namespace Goodreads
{
    /// <summary>
    /// The client API class for accessing the Goodreads API.
    /// </summary>
    public abstract class GoodreadsClient : IApiCredentialsInfoManager
    {
#pragma warning disable SA1401 // Fields must be private
        internal readonly IConnection _connection;
#pragma warning restore SA1401 // Fields must be private

        /// <summary>
        /// Create unauthorized goodreads client.
        /// </summary>
        /// <param name="apiKey">Your Goodreads API key.</param>
        /// <param name="apiSecret">Your Goodreads API secret.</param>
        /// <returns>Unauthorized goodreads client.</returns>
        public static IGoodreadsClient Create(string apiKey, string apiSecret)
        {
            return new UnauthorizedGoodreadsClient(apiKey, apiSecret, null, null);
        }

        /// <summary>
        /// Create authorized goodreads client.
        /// </summary>
        /// <param name="apiKey">Your Goodreads API key.</param>
        /// <param name="apiSecret">Your Goodreads API secret.</param>
        /// <param name="accessToken">The user's OAuth access token.</param>
        /// <param name="accessSecret">The user's OAuth access secret.</param>
        /// <returns>Authorized goodreads client</returns>
        public static IOAuthGoodreadsClient CreateAuth(string apiKey, string apiSecret, string accessToken, string accessSecret)
        {
            return new OAuthGoodreadsClient(apiKey, apiSecret, accessToken, accessSecret);
        }

        protected GoodreadsClient(string apiKey, string apiSecret, string accessToken, string accessSecret)
        {
            var apiCredentials = new ApiCredentials(apiKey, apiSecret, accessToken, accessSecret);
            _connection = new Connection(apiCredentials);
        }

        public ApiCredentials GetCredentials()
        {
            return _connection.Credentials;
        }
    }
}
