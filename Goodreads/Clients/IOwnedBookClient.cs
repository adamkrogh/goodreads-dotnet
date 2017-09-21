using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Owned book endpoint of the Goodreads API.
    /// </summary>
    public interface IOwnedBookClient
    {
        /// <summary>
        /// Get a list of owned books for the specified user.
        /// </summary>
        /// <param name="userId">A desire user Goodreads id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>A list of owned books for the specified user</returns>
        Task<PaginatedList<OwnedBook>> GetOwnedBooks(int userId, int page = 1);
    }
}
