using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the recommendation endpoint.
    /// </summary>
    public interface IOAuthRecommendationsEndpoint
    {
        /// <summary>
        /// Get information about a particular recommendation that one user made for another
        /// Includes comments and likes.
        /// </summary>
        /// <param name="id">A desire recommendation unique identifier.</param>
        /// <returns>A full information about desire recommendation.</returns>
        Task<Recommendation> GetRecommendation(long id);
    }
}
