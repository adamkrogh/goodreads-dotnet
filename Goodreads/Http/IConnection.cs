using Goodreads.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goodreads.Http
{
    internal interface IConnection
    {
        /// <summary>
        /// The Goodreads API credentials.
        /// </summary>
        ApiCredentials Credentials { get; }

        /// <summary>
        /// Execute a raw request to the Goodreads API.
        /// </summary>
        /// <param name="endpoint">The path of the API endpoint.</param>
        /// <param name="parameters">The RestSharp parameters to include in this request.</param>
        /// <param name="method">The HTTP method of this request.</param>
        /// <returns>An async task with the response from the request.</returns>
        Task<IRestResponse> ExecuteRaw(
            string endpoint,
            IEnumerable<Parameter> parameters,
            Method method = Method.GET);

        /// <summary>
        /// A common method for executing any request on the Goodreads API.
        /// </summary>
        /// <typeparam name="T">Generic type parameter of the data returned in the response.</typeparam>
        /// <param name="endpoint">The path of the API endpoint.</param>
        /// <param name="parameters">The RestSharp parameters to include in this request.</param>
        /// <param name="data">The data to include in the body of the request.</param>
        /// <param name="expectedRoot">The root XML node to start parsing from. Can by used to skip container elements if required.</param>
        /// <param name="method">The HTTP method of this request.</param>
        /// <returns>An async task with the response from the request.</returns>
        Task<T> ExecuteRequest<T>(
            string endpoint,
            IEnumerable<Parameter> parameters,
            object data = null,
            string expectedRoot = null,
            Method method = Method.GET)
            where T : ApiResponse, new();

        /// <summary>
        /// A specific method for executing json only request on the Goodreads API.
        /// </summary>
        /// <typeparam name="T">Generic type parameter of the data returned in the response.</typeparam>
        /// <param name="endpoint">The path of the API endpoint.</param>
        /// <param name="parameters">he RestSharp parameters to include in this request.</param>
        /// <returns>An async task with the response from the request.</returns>
        Task<T> ExecuteJsonRequest<T>(string endpoint, IEnumerable<Parameter> parameters);

        /// <summary>
        /// Gets a request token from Goodreads and builds an authorize URL to redirect the user to.
        /// </summary>
        /// <param name="callbackUrl">The callback URL that Goodreads will redirect back to.</param>
        /// <returns>The request token with authorize URL.</returns>
        Task<OAuthRequestToken> GetRequestToken(string callbackUrl);

        /// <summary>
        /// Get an access token from the Goodreads API using the given request token.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <returns>The access token.</returns>
        Task<OAuthAccessToken> GetAccessToken(OAuthRequestToken requestToken);
    }
}
