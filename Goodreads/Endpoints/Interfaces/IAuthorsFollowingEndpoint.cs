using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for author_following endpoint.
    /// </summary>
    public interface IOAuthAuthorsFollowingEndpoint
    {
        /// <summary>
        /// Follow an author.
        /// </summary>
        /// <param name="authorId">The Goodreads Id for the desired author.</param>
        /// <returns>A Goodreads author following model.</returns>
        Task<AuthorFollowing> Follow(long authorId);

        /// <summary>
        /// Unfollow an author.
        /// </summary>
        /// <param name="authorFollowingId">The Goodreads Id for the desired author.</param>
        /// <returns>True if the unfollow succeeded, false otherwise.</returns>
        Task<bool> Unfollow(long authorFollowingId);

        /// <summary>
        /// Show author following information.
        /// </summary>
        /// <param name="authorFollowingId">The Goodreads Id for the desired author.</param>
        /// <returns>A Goodreads author following model.</returns>
        Task<AuthorFollowing> Show(long authorFollowingId);
    }
}
