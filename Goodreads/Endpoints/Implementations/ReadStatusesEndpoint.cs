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
    internal sealed class ReadStatusesEndpoint : Endpoint, IOAuthReadStatusesEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadStatusesEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public ReadStatusesEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get information about a read status update.
        /// </summary>
        /// <param name="id">A desire read status unique identifier.</param>
        /// <returns>A full information about desire read status.</returns>
        public async Task<ReadStatus> GetReadStatus(long id)
        {
            var endpoint = $"read_statuses/{id}";
            return await Connection.ExecuteRequest<ReadStatus>(endpoint, null, null, "read_status").ConfigureAwait(false);
        }
    }
}
