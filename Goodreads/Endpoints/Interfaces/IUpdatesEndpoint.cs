using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Models.Request;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The OAuth API client class for the Updates endpoint of the Goodreads API.
    /// </summary>
    public interface IOAuthUpdatesEndpoint
    {
        /// <summary>
        /// Get your friend updates.
        /// </summary>
        /// <param name="type">An update type.</param>
        /// <param name="filter">An update filter.</param>
        /// <param name="maxUpdates">A maximum amount of updates.</param>
        /// <returns>Readonly friends update list.</returns>
        /// <remarks>Get the same data you see on your homepage.</remarks>
        Task<IReadOnlyList<Update>> GetFriendsUpdates(
            UpdateType? type = null,
            UpdateFilter? filter = null,
            int? maxUpdates = null);
    }
}
