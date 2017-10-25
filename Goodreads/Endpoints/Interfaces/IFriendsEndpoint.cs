using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for user friends endpoint.
    /// </summary>
    public interface IOAuthFriendsEndpoint
    {
        /// <summary>
        /// Sends a friend request to a user.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <returns>True if adding succeeded, false otherwise.</returns>
        Task<bool> AddFriend(long userId);

        /// <summary>
        /// Get the current user's friend requests.
        /// </summary>
        /// /// <param name="page">The desired page from the paginated list of friend requests.</param>
        /// <returns>A paginated list of friend requests.</returns>
        Task<PaginatedList<FriendRequest>> GetFriendRequests(int page = 1);

        /// <summary>
        /// Confirm or decline a friend request.
        /// </summary>
        /// <param name="friendRequestId">The friend request id.</param>
        /// <param name="response">The user response.</param>
        /// <returns>True if confirmation succeeded, otherwise - false.</returns>
        Task<bool> ConfirmRequest(long friendRequestId, bool response);
    }
}
