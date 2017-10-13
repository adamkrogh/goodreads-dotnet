using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    internal sealed class TopicsClient : EndpointClient, ITopicsClient
    {
        public TopicsClient(IConnection connection)
            : base(connection)
        {
        }

        public async Task<Topic> GetInfo(int topicId)
        {
            var endpoint = $"topic/show?id={topicId}";
            return await Connection.ExecuteRequest<Topic>(endpoint, new List<Parameter>(), null, "topic").ConfigureAwait(false);
        }
    }
}
