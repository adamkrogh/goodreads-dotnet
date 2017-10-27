using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for notification endpoint.
    /// </summary>
    internal sealed class NotificationsEndpoint : Endpoint, IOAuthNotificationsEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public NotificationsEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get any new notifications.
        /// </summary>
        /// <param name="page">The desired page from the paginated list of notifications.</param>
        /// <returns>A paginated list of notifications.</returns>
        public async Task<PaginatedList<Notification>> GetNotifications(int page)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<PaginatedList<Notification>>("notifications", parameters, null, "notifications", Method.GET).ConfigureAwait(false);
        }
    }
}
