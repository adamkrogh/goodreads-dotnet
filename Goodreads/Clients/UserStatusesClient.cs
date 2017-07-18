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
        public async Task<IReadOnlyList<UserStatus>> GetRecentUsersStatuses()
        {
            var result = await Connection.ExecuteRequest<PaginatedList<UserStatus>>(
                "user_status/index",
                new List<Parameter>(),
                null,
                "updates");

            return result?.List ?? new List<UserStatus>();
        }
    }
}
