using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for user friends endpoint.
    /// </summary>
    public interface IFriendsClient
    {
        /// <summary>
        /// Sends a friend request to a user.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <returns>True if adding succeeded, false otherwise.</returns>
        Task<bool> AddFriend(int userId);

        /// <summary>
        /// Get the current user's friend requests.
        /// </summary>
        /// /// <param name="page">The desired page from the paginated list of friend requests.</param>
        /// <returns>A paginated list of friend requests.</returns>
        /// <returns></returns>
        Task<PaginatedList<FriendRequest>> GetFriendRequests(int page = 1);

        /// <summary>
        /// Confirm or decline a friend request.
        /// </summary>
        /// <param name="friendRequestId">The friend request id.</param>
        /// <param name="response">The user response.</param>
        /// <returns>True if confirmation succeeded, otherwise - false.</returns>
        Task<bool> ConfirmRequest(int friendRequestId, bool response);

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
        Task<bool> ConfirmRecommendation(int recommendationId, bool response);
    }
}
