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

        /// <summary>
        /// Add a book to a shelf. This method can also be used to remove from shelf.
        /// </summary>
        /// <param name="shelf">Name of the shelf.</param>
        /// <param name="bookId">Id of the book to add to the shelf.</param>
        /// <param name="action">This null unless you're removing from a shelf. If removing, set this to 'remove'.</param>
        /// <returns>True if the add or remove succeeded, false otherwise.</returns>
        Task<bool> AddBookToShelf(string shelf, int bookId, string action = null);

        /// <summary>
        /// Add a list of books to many current user's shelves.
        /// </summary>
        /// <param name="shelves">List of shelf names.</param>
        /// <param name="bookIds">List of book ids.</param>
        /// <returns>True if the add succeeded, false otherwise.</returns>
        Task<bool> AddBooksToShelves(string[] shelves, int[] bookIds);
    }
}
