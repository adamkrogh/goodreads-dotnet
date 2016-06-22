using Goodreads.Clients;

namespace Goodreads
{
    /// <summary>
    /// The API interface for accessing the Goodreads API.
    /// </summary>
    public interface IGoodreadsClient
    {
        /// <summary>
        /// API Client for the Goodreads Authors endpoint.
        /// </summary>
        IAuthorsClient Authors { get; }
    }
}
