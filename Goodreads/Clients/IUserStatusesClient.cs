using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the user statuses endpoint of the Goodreads API.
    /// </summary>
    public interface IUserStatusesClient
    {
        /// <summary>
        /// Get most recent user statuses on the site.
        /// </summary>
        /// <returns>A list of the user statuses.</returns>
        /// [System.Diagnostics.CodeAnalysis.SuppressMessage(
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request.")]
        Task<IReadOnlyList<UserStatusSummary>> GetRecentUsersStatuses();

        /// <summary>
        /// Get information about a user status update.
        /// </summary>
        /// <param name="userStatusId">The user status id.</param>
        /// <returns>User status info.</returns>
        Task<UserStatus> GetUserStatus(int userStatusId);
    }
}
