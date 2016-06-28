using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions.MonoHttp;

namespace Goodreads.Http
{
    /// <summary>
    /// Represents credentials used to access the Goodreads API.
    /// </summary>
    public class ApiCredentials
    {
        private readonly IRestClient Client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCredentials"/> class.
        /// </summary>
        /// <param name="client">A RestClient connection to the Goodreads API.</param>
        /// <param name="apiKey">A Goodreads API key.</param>
        /// <param name="apiSecret">A Goodreads API secret.</param>
        public ApiCredentials(IRestClient client, string apiKey, string apiSecret) : this(client, apiKey, apiSecret, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCredentials"/> class.
        /// </summary>
        /// <param name="client">A RestClient connection to the Goodreads API.</param>
        /// <param name="apiKey">A Goodreads API key.</param>
        /// <param name="apiSecret">A Goodreads API secret.</param>
        /// <param name="oAuthToken">A user's OAuth token.</param>
        /// <param name="oAuthTokenSecret">A user's OAuth token secret.</param>
        public ApiCredentials(IRestClient client, string apiKey, string apiSecret, string oAuthToken, string oAuthTokenSecret)
        {
            Client = client;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            OAuthToken = oAuthToken;
            OAuthTokenSecret = oAuthTokenSecret;
        }

        /// <summary>
        /// The client's Goodreads API key.
        /// </summary>
        public string ApiKey { get; protected set; }

        /// <summary>
        /// The client's Goodreads API secret.
        /// </summary>
        public string ApiSecret { get; protected set; }

        /// <summary>
        /// The user's OAuth token.
        /// </summary>
        public string OAuthToken { get; protected set; }

        /// <summary>
        /// The user's OAuth token secret.
        /// </summary>
        public string OAuthTokenSecret { get; protected set; }

        /// <summary>
        /// The user's Goodreads Id.
        /// </summary>
        public int UserId { get; protected set; }

        /// <summary>
        /// Gets a request token from Goodreads and builds an authorize
        /// URL to redirect the user to.
        /// </summary>
        /// <param name="callbackUrl">The callback URL that Goodreads will redirect back to.</param>
        /// <returns>An authorize URL that the user can be redirected to.</returns>
        public string GetRequestTokenAndBuildAuthorizeUrl(string callbackUrl = null)
        {
            GetRequestToken();
            return BuildAuthorizeUrl(callbackUrl);
        }

        /// <summary>
        /// Build an OAuth authorization URL. This method uses
        /// the token passed into the GoodreadsClient constructor.
        /// Use the overload to change this.
        /// </summary>
        /// <param name="callbackUrl">The URL Goodreads will redirect back to.</param>
        /// <returns>A URL to authorize an OAuth request token.</returns>
        public string BuildAuthorizeUrl(string callbackUrl = null)
        {
            return BuildAuthorizeUrl(OAuthToken, callbackUrl);
        }

        /// <summary>
        /// Build an OAuth authorization URL with the given OAuth token and callback URL.
        /// </summary>
        /// <param name="oAuthToken">The OAuth request token to authorize.</param>
        /// <param name="callbackUrl">The URL Goodreads will redirect back to.</param>
        /// <returns>A URL to authorize an OAuth request token.</returns>
        public string BuildAuthorizeUrl(string oAuthToken, string callbackUrl)
        {
            var request = new RestRequest("oauth/authorize");
            request.AddParameter("oauth_token", oAuthToken);

            if (!string.IsNullOrEmpty(callbackUrl))
            {
                request.AddParameter("oauth_callback", callbackUrl);
            }

            return Client.BuildUri(request).ToString();
        }

        /// <summary>
        /// Get a request token from Goodreads using the API key and secret
        /// used in the client instantiation.
        /// </summary>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        public ApiCredentials GetRequestToken()
        {
            GetRequestToken(ApiKey, ApiSecret);
            return this;
        }

        /// <summary>
        /// Get a request token from the Goodreads API using the given API
        /// key and secret.
        /// </summary>
        /// <param name="apiKey">A Goodreads API key.</param>
        /// <param name="apiSecret">A Goodreads API secret.</param>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        public ApiCredentials GetRequestToken(string apiKey, string apiSecret)
        {
            Client.Authenticator = OAuth1Authenticator.ForRequestToken(apiKey, apiSecret);

            var request = new RestRequest("oauth/request_token", Method.GET);
            var response = Client.Execute(request);

            var queryString = HttpUtility.ParseQueryString(response.Content);
            var oAuthToken = queryString["oauth_token"];
            var oAuthTokenSecret = queryString["oauth_token_secret"];

            OAuthToken = oAuthToken;
            OAuthTokenSecret = oAuthTokenSecret;

            return this;
        }

        /// <summary>
        /// Get an access token from the Goodreads API.
        /// </summary>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        public ApiCredentials GetAccessToken()
        {
            return GetAccessToken(ApiKey, ApiSecret, OAuthToken, OAuthTokenSecret);
        }

        /// <summary>
        /// Get an access token from the Goodreads API using the given credentials.
        /// </summary>
        /// <param name="apiKey">A Goodreads API key.</param>
        /// <param name="apiSecret">A Goodreads API secret.</param>
        /// <param name="oAuthRequestToken">An OAuth request token that has been authorized.</param>
        /// <param name="oAuthRequestTokenSecret">An OAuth request token secret that has been authorized.</param>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        public ApiCredentials GetAccessToken(string apiKey, string apiSecret, string oAuthRequestToken, string oAuthRequestTokenSecret)
        {
            Client.Authenticator = OAuth1Authenticator.ForAccessToken(apiKey, apiSecret, oAuthRequestToken, oAuthRequestTokenSecret);

            var request = new RestRequest("oauth/access_token", Method.POST);
            var response = Client.Execute(request);

            var queryString = HttpUtility.ParseQueryString(response.Content);
            var oAuthToken = queryString["oauth_token"];
            var oAuthTokenSecret = queryString["oauth_token_secret"];

            OAuthToken = oAuthToken;
            OAuthTokenSecret = oAuthTokenSecret;

            return this;
        }
    }
}
