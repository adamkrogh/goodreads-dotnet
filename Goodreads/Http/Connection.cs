using Goodreads.Extensions;
using Goodreads.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Goodreads.Http
{
    internal class Connection : IConnection
    {
        private const string GoodreadsUrl = @"https://www.goodreads.com/";
        private const string GoodreadsUserAgent = @"goodreads-dotnet";
        private readonly IRestClient _client;

        /// <summary>
        /// Credentials for the Goodreads API.
        /// </summary>
        public ApiCredentials Credentials { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="client">A RestSharp client to use for this connection.</param>
        /// <param name="credentials">Credentials for use with the Goodreads API.</param>
        public Connection(ApiCredentials credentials)
        {
            _client = CreateClient(credentials);
            Credentials = credentials;
        }

        public async Task<IRestResponse> ExecuteRaw(
            string endpoint,
            IEnumerable<Parameter> parameters,
            Method method = Method.GET)
        {
            var request = BuildRequest(endpoint, parameters);
            request.Method = method;
            return await _client.ExecuteTaskRaw(request).ConfigureAwait(false);
        }

        public async Task<T> ExecuteRequest<T>(
            string endpoint,
            IEnumerable<Parameter> parameters,
            object data = null,
            string expectedRoot = null,
            Method method = Method.GET)
            where T : ApiResponse, new()
        {
            var request = BuildRequest(endpoint, parameters);
            request.RootElement = expectedRoot;
            request.Method = method;

            if (data != null && method != Method.GET)
            {
                request.RequestFormat = DataFormat.Xml;
                request.AddBody(data);
            }

            return await _client.ExecuteTask<T>(request).ConfigureAwait(false);
        }

        public async Task<T> ExecuteJsonRequest<T>(string endpoint, IEnumerable<Parameter> parameters)
        {
            var request = BuildRequest(endpoint, parameters);
            var response = await _client.ExecuteGetTaskAsync<T>(request).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<OAuthAccessToken> GetAccessToken(OAuthRequestToken requestToken)
        {
            _client.Authenticator = OAuth1Authenticator.ForAccessToken(
                Credentials.ApiKey,
                Credentials.ApiSecret,
                requestToken.Token,
                requestToken.Secret);

            var request = new RestRequest("oauth/access_token", Method.POST);
            var response = await _client.ExecuteTaskAsync(request).ConfigureAwait(false);

            var queryString = HttpUtility.ParseQueryString(response.Content);

            var oAuthToken = queryString["oauth_token"];
            var oAuthTokenSecret = queryString["oauth_token_secret"];

            return new OAuthAccessToken(oAuthToken, oAuthTokenSecret);
        }

        public async Task<OAuthRequestToken> GetRequestToken(string callbackUrl)
        {
            _client.Authenticator = OAuth1Authenticator.ForRequestToken(Credentials.ApiKey, Credentials.ApiSecret);

            var request = new RestRequest("oauth/request_token", Method.GET);
            var response = await _client.ExecuteTaskAsync(request).ConfigureAwait(false);

            var queryString = HttpUtility.ParseQueryString(response.Content);

            var oAuthToken = queryString["oauth_token"];
            var oAuthTokenSecret = queryString["oauth_token_secret"];
            var authorizeUrl = BuildAuthorizeUrl(oAuthToken, callbackUrl);

            return new OAuthRequestToken(oAuthToken, oAuthTokenSecret, authorizeUrl);
        }

        private string BuildAuthorizeUrl(string oauthToken, string callbackUrl)
        {
            var request = new RestRequest("oauth/authorize");
            request.AddParameter("oauth_token", oauthToken);

            if (!string.IsNullOrEmpty(callbackUrl))
            {
                request.AddParameter("oauth_callback", callbackUrl);
            }

            return _client.BuildUri(request).ToString();
        }

        private static IRestRequest BuildRequest(string endpoint, IEnumerable<Parameter> parameters)
        {
            var request = new RestRequest(endpoint);

            foreach (var parameter in parameters ?? Enumerable.Empty<Parameter>())
            {
                request.AddParameter(parameter);
            }

            return request;
        }

        private static IRestClient CreateClient(ApiCredentials credentials)
        {
            var client = new RestClient(new Uri(GoodreadsUrl))
            {
                UserAgent = GoodreadsUserAgent
            };

            client.AddDefaultParameter("key", credentials.ApiKey, ParameterType.QueryString);
            client.AddDefaultParameter("format", "xml", ParameterType.QueryString);

            if (!string.IsNullOrEmpty(credentials.OAuthToken) && !string.IsNullOrEmpty(credentials.OAuthTokenSecret))
            {
                client.Authenticator =
                    OAuth1Authenticator.ForProtectedResource(
                    credentials.ApiKey,
                    credentials.ApiSecret,
                    credentials.OAuthToken,
                    credentials.OAuthTokenSecret);
            }

            return client;
        }
    }
}
