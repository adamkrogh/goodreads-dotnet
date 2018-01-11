using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Models.Request;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the Books endpoint.
    /// </summary>
    public interface IBooksEndpoint
    {
        /// <summary>
        /// Get book information by ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the desired book.</param>
        /// <returns>An async task returning the desired book information.</returns>
        Task<Book> GetByIsbn(string isbn);

        /// <summary>
        /// Get book information by Goodreads book id.
        /// </summary>
        /// <param name="bookId">The Goodreads book id.</param>
        /// <returns>Information about the Goodreads book, null if not found.</returns>
        Task<Book> GetByBookId(long bookId);

        /// <summary>
        /// Get book information by book title.
        /// Include an author name for increased accuracy.
        /// </summary>
        /// <param name="title">The book title to find.</param>
        /// <param name="author">The author of the book, optional but include it for increased accuracy.</param>
        /// <param name="rating">Show only reviews with a particular rating.</param>
        /// <returns>Information about the Goodreads book, null if not found.</returns>
        Task<Book> GetByTitle(string title, string author = null, int? rating = null);

        /// <summary>
        /// Search Goodreads for books (returned as <see cref="Work"/> objects).
        /// </summary>
        /// <param name="searchTerm">The search term to search Goodreads with.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <param name="searchField">The book fields to apply the search term against.</param>
        /// <returns>A paginated list of <see cref="Work"/> object matching the given search criteria.</returns>
        Task<PaginatedList<Work>> Search(string searchTerm, int page = 1, BookSearchField searchField = BookSearchField.All);

        /// <summary>
        /// Gets a single Goodreads book id for the given ISBN10 or ISBN13.
        /// </summary>
        /// <param name="isbn">The ISBN number to fetch a book it for. Can be ISBN10 or ISBN13.</param>
        /// <returns>A Goodreads book id if found, null otherwise.</returns>
        Task<long?> GetBookIdForIsbn(string isbn);

        /// <summary>
        /// Converts a list of ISBNs (ISBN10 or ISBN13) to Goodreads book ids.
        /// The ordering and size of the list is kept consistent with missing
        /// ISBNs substituted with null.
        /// </summary>
        /// <param name="isbns">The list of ISBNs to convert.</param>
        /// <returns>A list of Goodreads book ids (with null elements if an ISBN wasn't found).</returns>
        Task<IReadOnlyList<long?>> GetBookIdsForIsbns(IReadOnlyList<string> isbns);

        /// <summary>
        /// Converts a list of Goodreads book ids to work ids.
        /// The ordering and size of the list is kept consistent with missing
        /// book ids substituted with null.
        /// </summary>
        /// <param name="bookIds">The list of Goodreads book ids to convert.</param>
        /// <returns>A list of work ids corresponding to the given book ids.</returns>
        Task<IReadOnlyList<long?>> GetWorkIdsForBookIds(IReadOnlyList<long> bookIds);

        /// <summary>
        /// Get review statistics for a list of books by ISBN10 or ISBN13.
        /// </summary>
        /// <param name="isbns">A list of ISBN10 or ISBN13s to retrieve stats for.</param>
        /// <returns>A list of review stats for the given ISBNs.</returns>
        Task<IReadOnlyList<ReviewStats>> GetReviewStatsForIsbns(IReadOnlyList<string> isbns);
    }

    /// <summary>
    /// API OAuth client for the Books endpoint.
    /// </summary>
    public interface IOAuthBooksEndpoint : IBooksEndpoint
    {
        /// <summary>
        /// Gets a paginated list of books written by the given author.
        /// </summary>
        /// <param name="authorId">The Goodreads author id.</param>
        /// <param name="page">The desired page from the paginated list of books.</param>
        /// <returns>A paginated list of books written by the author.</returns>
        Task<PaginatedList<Book>> GetListByAuthorId(long authorId, int page = 1);
    }
}
