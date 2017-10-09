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
    internal sealed class FriendsClient : IFriendsClient
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
                "requests/friend_requests",
                Method.GET);
        }

        /// <summary>
        /// Confirm or decline a friend request.
        /// </summary>
        /// <param name="friendRequestId">The friend request id.</param>
        /// <param name="response">The user response.</param>
        /// <returns>True if confirmation succeeded, otherwise - false.</returns>
        public async Task<bool> ConfirmRequest(int friendRequestId, bool response)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = friendRequestId, Type = ParameterType.QueryString },
                new Parameter { Name = "response", Value = response ? "Y" : "N", Type = ParameterType.QueryString }
            };

            var result = await Connection.ExecuteRaw("friend/confirm_request", parameters, Method.POST);

            return result.StatusCode == HttpStatusCode.NoContent;
        }

        /// <summary>
        /// Confirm or decline a friend recommendation.
        /// </summary>
        /// <param name="recommendationId">The friend recommendation id.</param>
        /// <param name="response">The user response.</param>
        /// <returns>True if confirmation succeeded, otherwise - false.</returns>
        /// <remarks>
        /// ATTENTION! Seems that Goodreads endpoint is not working as describe in an official documentation.
        /// Moreover I think method is not working at all.
        /// There is not ability to approve recommendation in web version. An user need to add a book to his shelf instead.
        /// Also I can't ignore recommendation using the Goodreads API.
        /// </remarks>
        public async Task<bool> ConfirmRecommendation(int recommendationId, bool response)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = recommendationId, Type = ParameterType.QueryString },
                new Parameter { Name = "response", Value = response ? "Y" : "N", Type = ParameterType.QueryString }
            };

            var result = await Connection.ExecuteRaw("friend/confirm_recommendation", parameters, Method.POST);

            return result.StatusCode == HttpStatusCode.NoContent;
        }
    }
}
