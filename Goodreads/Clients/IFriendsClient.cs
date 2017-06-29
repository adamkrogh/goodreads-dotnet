using System.Threading.Tasks;

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
    }
}
