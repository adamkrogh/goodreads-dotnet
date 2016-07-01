using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the Authors endpoint.
    /// </summary>
    public interface IAuthorsClient
    {
        /// <summary>
        /// Get the author information for the given Id.
        /// </summary>
        /// <param name="authorId">The Goodreads Id for the desired author.</param>
        /// <returns>An async task returning the desired author information.</returns>
        Task<Author> GetByAuthorId(int authorId);
    }
}
