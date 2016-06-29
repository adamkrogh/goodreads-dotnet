using System.Threading.Tasks;
using Goodreads.Models.Request;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Users endpoint of the Goodreads API.
    /// </summary>
    public interface IUsersClient
    {
        /// <summary>
        /// Gets a paginated list of friends for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <param name="sort">The sort order of the paginated list.</param>
        /// <returns>A paginated list of the user's shelves.</returns>
        Task<PaginatedList<User>> GetListOfFriends(int userId, int page = 1, SortFriendsList sort = SortFriendsList.FirstName);
    }
}
