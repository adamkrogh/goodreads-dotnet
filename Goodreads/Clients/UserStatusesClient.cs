using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the user statuses endpoint of the Goodreads API.
    /// </summary>
    public class UserStatusesClient : IUserStatusesClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatusesClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public UserStatusesClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Get most recent user statuses on the site.
        /// </summary>
        /// <returns>A list of the user statuses.</returns>
        public async Task<IReadOnlyList<UserStatusSummary>> GetRecentUsersStatuses()
        {
            var result = await Connection.ExecuteRequest<PaginatedList<UserStatusSummary>>(
                "user_status/index",
                new List<Parameter>(),
                null,
                "updates");

            return result?.List ?? new List<UserStatusSummary>();
        }

        /// <summary>
        /// Get information about a user status update.
        /// </summary>
        /// <param name="userStatusId">The user status id.</param>
        /// <returns>User status info.</returns>
        public async Task<UserStatus> GetUserStatus(int userStatusId)
        {
            var endpoint = $"user_status/show/{userStatusId}";
           return await Connection.ExecuteRequest<UserStatus>(endpoint, new List<Parameter>(), null, "user_status");
        }
    }
}
