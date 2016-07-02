using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Extensions;
using Goodreads.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions.MonoHttp;

namespace Goodreads.Http
{
    /// <summary>
    /// A common connection class to the Goodreads API.
    /// </summary>
    public class Connection : IConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="client">A RestSharp client to use for this connection.</param>
        /// <param name="credentials">Credentials for use with the Goodreads API.</param>
        public Connection(IRestClient client, ApiCredentials credentials)
        {
            Client = client;
            Credentials = credentials;
        }

        #region IConnection Members

        /// <summary>
        /// The RestSharp client for this connection.
        /// </summary>
        public IRestClient Client { get; private set; }

        /// <summary>
        /// Credentials for the Goodreads API.
        /// </summary>
        public ApiCredentials Credentials { get; private set; }

        /// <summary>
        /// Execute a raw request to the Goodreads API.
        /// </summary>
        /// <param name="endpoint">The path of the API endpoint.</param>
        /// <param name="parameters">The RestSharp parameters to include in this request.</param>
        /// <param name="method">The HTTP method of this request.</param>
        /// <returns>An async task with the response from the request.</returns>
        public async Task<IRestResponse> ExecuteRaw(
            string endpoint,
            IList<Parameter> parameters,
            Method method = Method.GET)
        {
            var request = BuildRequest(endpoint, parameters);
            request.Method = method;
            return await Client.ExecuteTaskRaw(request).ConfigureAwait(false);
        }

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
        public async Task<T> ExecuteRequest<T>(
            string endpoint,
            IList<Parameter> parameters,
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

            return await Client.ExecuteTask<T>(request).ConfigureAwait(false);
        }

        #endregion

        /// <summary>
        /// Build a rest request to the given endpoint, using the given parameters.
        /// </summary>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="parameters">The parameters for this request.</param>
        /// <returns>A RestRequest for this connection.</returns>
        public IRestRequest BuildRequest(string endpoint, IEnumerable<Parameter> parameters)
        {
            var request = new RestRequest(endpoint);

            if (parameters == null)
            {
                return request;
            }

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter);
            }

            return request;
        }
    }
}
