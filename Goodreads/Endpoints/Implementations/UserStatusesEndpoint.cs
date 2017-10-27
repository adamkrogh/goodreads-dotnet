using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the user statuses endpoint of the Goodreads API.
    /// </summary>
    internal sealed class UserStatusesEndpoint : Endpoint, IOAuthUserStatusesEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatusesEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public UserStatusesEndpoint(IConnection connection)
            : base(connection)
        {
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
                "updates").ConfigureAwait(false);

            return result?.List ?? new List<UserStatusSummary>();
        }

        /// <summary>
        /// Get information about a user status update.
        /// </summary>
        /// <param name="userStatusId">The user status id.</param>
        /// <returns>User status info.</returns>
        public async Task<UserStatus> GetUserStatus(long userStatusId)
        {
            var endpoint = $"user_status/show/{userStatusId}";
           return await Connection.ExecuteRequest<UserStatus>(endpoint, new List<Parameter>(), null, "user_status").ConfigureAwait(false);
        }

        /// <summary>
        /// Create a status updates for members.
        /// </summary>
        /// <param name="bookId">Id of the book being reviewed.</param>
        /// <param name="page">Page of the book.</param>
        /// <param name="percent">Percent complete.</param>
        /// <param name="comment">The status update comment.</param>
        /// <returns>The new user status model.</returns>
        public async Task<long> Create(long bookId, int? page, int? percent, string comment)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "user_status[book_id]", Value = bookId, Type = ParameterType.QueryString }
            };

            if (page.HasValue)
            {
                parameters.Add(new Parameter { Name = "user_status[page]", Value = page.Value, Type = ParameterType.QueryString });
            }

            if (percent.HasValue)
            {
                parameters.Add(new Parameter { Name = "user_status[percent]", Value = percent.Value, Type = ParameterType.QueryString });
            }

            if (!string.IsNullOrEmpty(comment))
            {
                parameters.Add(new Parameter { Name = "user_status[body]", Value = comment, Type = ParameterType.QueryString });
            }

            var status = await Connection.ExecuteRequest<UserStatusSummary>("user_status", parameters, null, "user-status", Method.POST).ConfigureAwait(false);

            return status.Id;
        }

        /// <summary>
        /// Delete a status update.
        /// </summary>
        /// <param name="userStatusId">The specified user status id.</param>
        /// <returns>True if delete succeeded, false otherwise.</returns>
        public async Task<bool> Delete(long userStatusId)
        {
            var endpoint = $"user_status/destroy/{userStatusId}";
            var response = await Connection.ExecuteRaw(endpoint, null, Method.POST).ConfigureAwait(false);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
