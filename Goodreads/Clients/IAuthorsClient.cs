using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    public interface IAuthorsClient
    {
        /// <summary>
        /// Retrieve a single author
        /// </summary>
        Task<Author> Get(int authorId);
    }
}
