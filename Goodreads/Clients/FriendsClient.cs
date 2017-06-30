using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for user friends endpoint.
    /// </summary>
    public class FriendsClient : IFriendsClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendsClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public FriendsClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Sends a friend request to a user.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <returns>True if adding succeeded, false otherwise.</returns>
        public async Task<bool> AddFriend(int userId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = userId, Type = ParameterType.QueryString }
            };

            var response = await Connection.ExecuteRaw("friend/add_as_friend", parameters, Method.POST);

            return response.StatusCode == HttpStatusCode.Created;
        }

        /// <summary>
        /// Get the current user's friend requests.
        /// </summary>
        /// /// <param name="page">The desired page from the paginated list of friend requests.</param>
        /// <returns>A paginated list of friend requests.</returns>
        public async Task<PaginatedList<FriendRequest>> GetFriendRequests(int page = 1)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<PaginatedList<FriendRequest>>(
                "friend/requests",
                parameters,
                null,
                "friend_requests",
                Method.GET);
        }
    }
}
