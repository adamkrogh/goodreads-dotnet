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
    internal sealed class NotificationsClient : INotificationsClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public NotificationsClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Get any new notifications.
        /// </summary>
        /// <param name="page">The desired page from the paginated list of notifications.</param>
        /// <returns>A paginated list of notifications.</returns>
        public Task<PaginatedList<Notification>> GetNotifications(int page = 1)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<PaginatedList<Notification>>("notifications", parameters, null, "notifications", Method.GET);
        }
    }
}
