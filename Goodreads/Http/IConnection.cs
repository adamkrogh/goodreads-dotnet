using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Models;
using RestSharp;

namespace Goodreads.Http
{
    /// <summary>
    /// The interface for a connection to the Goodreads API.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// The RestSharp client for this connection.
        /// </summary>
        IRestClient Client { get; }

        /// <summary>
        /// Credentials for the Goodreads API.
        /// </summary>
        ApiCredentials Credentials { get; }

        /// <summary>
        /// Determines if the connection has been authenticated via OAuth, or not.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Execute a raw request to the Goodreads API.
        /// </summary>
        /// <param name="endpoint">The path of the API endpoint.</param>
        /// <param name="parameters">The RestSharp parameters to include in this request.</param>
        /// <param name="method">The HTTP method of this request.</param>
        /// <returns>An async task with the response from the request.</returns>
        Task<IRestResponse> ExecuteRaw(
            string endpoint,
            IList<Parameter> parameters,
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
            IList<Parameter> parameters,
            object data = null,
            string expectedRoot = null,
            Method method = Method.GET)
            where T : ApiResponse, new();

        /// <summary>
        /// Build a rest request to the given endpoint, using the given parameters.
        /// </summary>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="parameters">The parameters for this request.</param>
        /// <returns>A RestRequest for this connection.</returns>
        IRestRequest BuildRequest(string endpoint, IEnumerable<Parameter> parameters);
    }
}
