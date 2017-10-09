using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions.MonoHttp;

namespace Goodreads.Http
{
    /// <summary>
    /// Represents credentials used to access the Goodreads API.
    /// </summary>
    public sealed class ApiCredentials
    {
        private readonly IRestClient Client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCredentials"/> class.
        /// </summary>
        /// <param name="client">A RestClient connection to the Goodreads API.</param>
        /// <param name="apiKey">A Goodreads API key.</param>
        /// <param name="apiSecret">A Goodreads API secret.</param>
        /// <param name="oauthToken">A user's OAuth token.</param>
        /// <param name="oauthTokenSecret">A user's OAuth token secret.</param>
        public ApiCredentials(IRestClient client, string apiKey, string apiSecret, string oauthToken, string oauthTokenSecret)
        {
            Client = client;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            OauthToken = oauthToken;
            OauthTokenSecret = oauthTokenSecret;
        }

        /// <summary>
        /// The client's Goodreads API key.
        /// </summary>
        public string ApiKey { get; private set; }

        /// <summary>
        /// The client's Goodreads API secret.
        /// </summary>
        public string ApiSecret { get; private set; }

        /// <summary>
        /// The user's OAuth token.
        /// </summary>
        public string OauthToken { get; private set; }

        /// <summary>
        /// The user's OAuth token secret.
        /// </summary>
        public string OauthTokenSecret { get; private set; }

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
            return BuildAuthorizeUrl(OauthToken, callbackUrl);
        }

        /// <summary>
        /// Build an OAuth authorization URL with the given OAuth token and callback URL.
        /// </summary>
        /// <param name="oauthToken">The OAuth request token to authorize.</param>
        /// <param name="callbackUrl">The URL Goodreads will redirect back to.</param>
        /// <returns>A URL to authorize an OAuth request token.</returns>
        public string BuildAuthorizeUrl(string oauthToken, string callbackUrl)
        {
            var request = new RestRequest("oauth/authorize");
            request.AddParameter("oauth_token", oauthToken);

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

            OauthToken = oAuthToken;
            OauthTokenSecret = oAuthTokenSecret;

            return this;
        }

        /// <summary>
        /// Get an access token from the Goodreads API.
        /// </summary>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        public ApiCredentials GetAccessToken()
        {
            return GetAccessToken(ApiKey, ApiSecret, OauthToken, OauthTokenSecret);
        }

        /// <summary>
        /// Get an access token from the Goodreads API using the given credentials.
        /// </summary>
        /// <param name="apiKey">A Goodreads API key.</param>
        /// <param name="apiSecret">A Goodreads API secret.</param>
        /// <param name="oauthRequestToken">An OAuth request token that has been authorized.</param>
        /// <param name="oauthRequestTokenSecret">An OAuth request token secret that has been authorized.</param>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        public ApiCredentials GetAccessToken(string apiKey, string apiSecret, string oauthRequestToken, string oauthRequestTokenSecret)
        {
            Client.Authenticator = OAuth1Authenticator.ForAccessToken(apiKey, apiSecret, oauthRequestToken, oauthRequestTokenSecret);

            var request = new RestRequest("oauth/access_token", Method.POST);
            var response = Client.Execute(request);

            var queryString = HttpUtility.ParseQueryString(response.Content);
            var oAuthToken = queryString["oauth_token"];
            var oAuthTokenSecret = queryString["oauth_token_secret"];

            OauthToken = oAuthToken;
            OauthTokenSecret = oAuthTokenSecret;

            return this;
        }
    }
}
