using System.Threading.Tasks;
using Goodreads.Models.Request;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Users endpoint of the Goodreads API.
    /// </summary>
    public interface IUsersEndpoint
    {
        /// <summary>
        /// Gets the public information for a Goodreads user.
        /// </summary>
        /// <param name="userId">The Goodreads user id of the user to fetch.</param>
        /// <returns>Information about the desired user.</returns>
        Task<User> GetByUserId(long userId);

        /// <summary>
        /// Gets the public information for a Goodreads user by username.
        /// Note that usernames are optional in Goodreads.
        /// </summary>
        /// <param name="username">The Goodreads username of the user to fetch.</param>
        /// <returns>Information about the desired user.</returns>
        Task<User> GetByUsername(string username);

        /// <summary>
        /// Gets a paginated list of friends for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <param name="sort">The sort order of the paginated list.</param>
        /// <returns>A paginated list of the user summary information for their friends.</returns>
        Task<PaginatedList<UserSummary>> GetListOfFriends(long userId, int page = 1, SortFriendsList sort = SortFriendsList.FirstName);

        /// <summary>
        /// Get an people the given user is following.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>People the given user is following.</returns>
        Task<PaginatedList<UserFollowing>> GetUserFollowing(long userId, int page = 1);

        /// <summary>
        /// Get given user's followers.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>The specified user's followers.</returns>
        Task<PaginatedList<UserFollowers>> GetUsersFollowers(long userId, int page = 1);
    }

    /// <summary>
    /// The OAuth client class for the Users endpoint of the Goodreads API.
    /// </summary>
    public interface IOAuthUsersEndpoint : IUsersEndpoint
    {
        /// <summary>
        /// Gets the Goodreads user id of the authenticated connection.
        /// If the client isn't using OAuth, this returns null.
        /// </summary>
        /// <returns>The user id of the authenticated user. Null if just using the public API.</returns>
        Task<long> GetAuthenticatedUserId();

        /// <summary>
        /// Get stats comparing your books to another member's.
        /// </summary>
        /// <param name="userId">A desired user if to ompare.</param>
        /// <returns>A compare books result.</returns>
        Task<CompareBooksResult> CompareUserBooks(long userId);
    }
}
