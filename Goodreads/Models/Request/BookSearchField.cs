using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The fields to search for books by.
    /// </summary>
    [QueryParameterKey("search[field]")]
    public enum BookSearchField
    {
        /// <summary>
        /// Search books by all fields.
        /// </summary>
        [QueryParameterValue("all")]
        All,

        /// <summary>
        /// Search by the title of the book.
        /// </summary>
        [QueryParameterValue("title")]
        Title,

        /// <summary>
        /// Search by the author of the book.
        /// </summary>
        [QueryParameterValue("author")]
        Author
    }
}
