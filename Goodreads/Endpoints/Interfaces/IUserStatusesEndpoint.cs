using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the user statuses endpoint of the Goodreads API.
    /// </summary>
    public interface IUserStatusesEndpoint
    {
        /// <summary>
        /// Get most recent user statuses on the site.
        /// </summary>
        /// <returns>A list of the user statuses.</returns>
        Task<IReadOnlyList<UserStatusSummary>> GetRecentUsersStatuses();

        /// <summary>
        /// Get information about a user status update.
        /// </summary>
        /// <param name="userStatusId">The user status id.</param>
        /// <returns>User status info.</returns>
        Task<UserStatus> GetUserStatus(long userStatusId);
    }

     /// <summary>
    /// The OAuth client class for the user statuses endpoint of the Goodreads API.
    /// </summary>
    public interface IOAuthUserStatusesEndpoint : IUserStatusesEndpoint
    {
        /// <summary>
        /// Create a status updates for members.
        /// </summary>
        /// <param name="bookId">Id of the book being reviewed.</param>
        /// <param name="page">Page of the book.</param>
        /// <param name="percent">Percent complete.</param>
        /// <param name="comment">The status update comment.</param>
        /// <returns>Id of a new user status.</returns>
        Task<long> Create(long bookId, int? page = null, int? percent = null, string comment = null);

        /// <summary>
        /// Delete a status update.
        /// </summary>
        /// <param name="userStatusId">The specified user status id.</param>
        /// <returns>True if delete succeeded, false otherwise.</returns>
        Task<bool> Delete(long userStatusId);
    }
}
