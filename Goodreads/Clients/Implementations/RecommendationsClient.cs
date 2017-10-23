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
    internal sealed class RecommendationsClient : EndpointClient, IRecommendationsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationsClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public RecommendationsClient(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get information about a particular recommendation that one user made for another
        /// Includes comments and likes.
        /// </summary>
        /// <param name="id">A desire recommendation unique identifier.</param>
        /// <returns>A full information about desire recommendation.</returns>
        async Task<Recommendation> IRecommendationsClient.GetRecommendation(long id)
        {
            var endpoint = $"recommendations/{id}";
            var parameters = new List<Parameter>();

            return await Connection.ExecuteRequest<Recommendation>(endpoint, parameters, null, "recommendation");
        }
    }
}
