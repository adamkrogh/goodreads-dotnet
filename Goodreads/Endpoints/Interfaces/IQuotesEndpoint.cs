using System.Threading.Tasks;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Quotes endpoint of the Goodreads API.
    /// </summary>
    public interface IOAuthQuotesEndpoint
    {
        /// <summary>
        /// Add a quote.
        /// </summary>
        /// <param name="authorId">The Goodreads author id.</param>
        /// <param name="authorName">Name of the quote author.</param>
        /// <param name="quote">The quote.</param>
        /// <param name="bookId">Id of the book from which the quote was taken.</param>
        /// <param name="isbn">ISBN of the book from which the quote was taken.
        /// This will not override the book_id if it was provided.</param>
        /// <returns>True if adding succeeded, false otherwise.</returns>
        Task<bool> Add(
            long authorId,
            string authorName,
            string quote,
            long? bookId = null,
            string isbn = null);
    }
}
