using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the read statuses endpoint.
    /// </summary>
    internal sealed class ReadStatusesClient : EndpointClient, IReadStatusesClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadStatusesClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public ReadStatusesClient(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get information about a read status update.
        /// </summary>
        /// <param name="id">A desire read status unique identifier.</param>
        /// <returns>A full information about desire read status.</returns>
        async Task<ReadStatus> IReadStatusesClient.GetReadStatus(long id)
        {
            var endpoint = $"read_statuses/{id}";
            var parameters = new List<Parameter>();

            return await Connection.ExecuteRequest<ReadStatus>(endpoint, parameters, null, "read_status");
        }
    }
}
