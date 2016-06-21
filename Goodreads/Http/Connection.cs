using Goodreads.Extensions;
using RestSharp;
using RestSharp.Serializers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goodreads.Http
{
    public class Connection : IConnection
    {
        public Connection(IRestClient client)
        {
            Client = client;
        }

        #region IConnection Members

        public IRestClient Client { get; private set; }

        public async Task<IRestResponse> ExecuteRaw(string endpoint, IList<Parameter> parameters,
            Method method = Method.GET)
        {
            var request = BuildRequest(endpoint, parameters);
            request.Method = method;
            return await Client.ExecuteTaskRaw(request).ConfigureAwait(false);
        }

        public async Task<T> ExecuteRequest<T>(string endpoint, IList<Parameter> parameters,
            object data = null, string expectedRoot = null, Method method = Method.GET)
            where T : new()
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

        private IRestRequest BuildRequest(string endpoint, IEnumerable<Parameter> parameters)
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
