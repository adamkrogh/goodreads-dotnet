using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the recommendation endpoint.
    /// </summary>
    internal sealed class RecommendationsEndpoint : Endpoint, IOAuthRecommendationsEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationsEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public RecommendationsEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get information about a particular recommendation that one user made for another
        /// Includes comments and likes.
        /// </summary>
        /// <param name="id">A desire recommendation unique identifier.</param>
        /// <returns>A full information about desire recommendation.</returns>
        public async Task<Recommendation> GetRecommendation(long id)
        {
            var endpoint = $"recommendations/{id}";
            return await Connection.ExecuteRequest<Recommendation>(endpoint, null, null, "recommendation").ConfigureAwait(false);
        }
    }
}
