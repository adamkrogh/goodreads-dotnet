using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Goodreads.Http
{
    public interface IConnection
    {
        IRestClient Client { get; }

        Task<IRestResponse> ExecuteRaw(string endpoint, IList<Parameter> parameters, Method method = Method.GET);

        Task<T> ExecuteRequest<T>(string endpoint, IList<Parameter> parameters,
            object data = null, string expectedRoot = null, Method method = Method.GET) where T : new();
    }
}
