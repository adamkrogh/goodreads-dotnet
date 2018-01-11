using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API OAuth client for user notifications endpoint.
    /// </summary>
    public interface IOAuthNotificationsEndpoint
    {
        /// <summary>
        /// Get any new notifications.
        /// </summary>
        /// <param name="page">The desired page from the paginated list of notifications.</param>
        /// <returns>A paginated list of notifications.</returns>
        Task<PaginatedList<Notification>> GetNotifications(int page = 1);
    }
}
