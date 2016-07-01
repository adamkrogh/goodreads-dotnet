using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the Shelves endpoint.
    /// </summary>
    public interface IShelvesClient
    {
        /// <summary>
        /// Gets a paginated list of shelves for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>A paginated list of the user's shelves.</returns>
        Task<PaginatedList<UserShelf>> GetListOfUserShelves(int userId, int page = 1);
    }
}
