using System;
using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Owned book endpoint of the Goodreads API.
    /// </summary>
    public interface IOAuthOwnedBooksEndpoint
    {
        /// <summary>
        /// Get a list of owned books for the specified user.
        /// </summary>
        /// <param name="userId">A desire user Goodreads id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>A list of owned books for the specified user</returns>
        Task<PaginatedList<OwnedBook>> GetOwnedBooks(long userId, int page = 1);

        /// <summary>
        /// Get an owned book, including the current owner's user id.
        /// </summary>
        /// <param name="ownedBookId">A desire owned book id.</param>
        /// <returns>An owned book information.</returns>
        Task<OwnedBook> GetOwnedBookInfo(long ownedBookId);

        /// <summary>
        /// Adds a book to user's list of owned books.
        /// </summary>
        /// <param name="bookId">Id of the book.</param>
        /// <param name="code">An one of 10 (brand new), 20 (like new), 30 (very good), 40 (good), 50 (acceptable), 60 (poor).</param>
        /// <param name="description">A description of book's.</param>
        /// <param name="purchaseDate">A date when book was purchased.</param>
        /// <param name="purchaseLocation">A location where this book was purchased.</param>
        /// <param name="bcid">BookCrossing id (BCID).</param>
        /// <returns>An owned book object.</returns>
        Task<OwnedBookSummary> AddOwnedBook(
            long bookId,
            int code = 10,
            string description = null,
            DateTime? purchaseDate = null,
            string purchaseLocation = null,
            int? bcid = null);

        /// <summary>
        /// Deletes a book a user owns.
        /// </summary>
        /// <param name="ownedBookId">Id of the owned book.</param>
        /// <returns>True if deleting successed, otherwise false.</returns>
        Task<bool> DeleteOwnedBook(long ownedBookId);
    }
}
