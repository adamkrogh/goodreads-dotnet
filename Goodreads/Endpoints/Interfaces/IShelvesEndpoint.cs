using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the Shelves endpoint.
    /// </summary>
    public interface IShelvesEndpoint
    {
        /// <summary>
        /// Gets a paginated list of shelves for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>A paginated list of the user's shelves.</returns>
        Task<PaginatedList<UserShelf>> GetListOfUserShelves(long userId, int page = 1);
    }

    /// <summary>
    /// API OAuth client for the Shelves endpoint.
    /// </summary>
    public interface IOAuthShelvesEndpoint : IShelvesEndpoint
    {
        /// <summary>
        /// Add a book to a shelf. This method can also be used to remove from shelf.
        /// </summary>
        /// <param name="shelf">Name of the shelf.</param>
        /// <param name="bookId">Id of the book to add to the shelf.</param>
        /// <param name="action">This null unless you're removing from a shelf. If removing, set this to 'remove'.</param>
        /// <returns>True if the add or remove succeeded, false otherwise.</returns>
        Task<bool> AddBookToShelf(string shelf, long bookId, string action = null);

        /// <summary>
        /// Add a list of books to many current user's shelves.
        /// </summary>
        /// <param name="shelves">List of shelf names.</param>
        /// <param name="bookIds">List of book ids.</param>
        /// <returns>True if the add succeeded, false otherwise.</returns>
        Task<bool> AddBooksToShelves(string[] shelves, long[] bookIds);

        /// <summary>
        /// Add a user book shelf.
        /// </summary>
        /// <param name="shelf">Name of the user shelf.</param>
        /// <param name="exclusive">Determine whether shelf is exclusive.</param>
        /// <param name="sortable">Determine whether shelf is sortable.</param>
        /// <param name="featured">Determine whether shelf is featured.</param>
        /// <returns>The created user shelf.</returns>
        Task<UserShelf> AddShelf(string shelf, bool exclusive = false, bool sortable = false, bool featured = false);

        /// <summary>
        /// Edit a user book shelf.
        /// </summary>
        /// /// <param name="shelfId">Id of the user shelf.</param>
        /// <param name="shelf">Name of the shelf.</param>
        /// <param name="exclusive">Determine whether shelf is exclusive.</param>
        /// <param name="sortable">Determine whether shelf is sortable.</param>
        /// <param name="featured">Determine whether shelf is featured.</param>
        /// <returns>True if the edit succeeded, false otherwise.</returns>
        Task<bool> EditShelf(long shelfId, string shelf, bool exclusive = false, bool sortable = false, bool featured = false);
    }
}
