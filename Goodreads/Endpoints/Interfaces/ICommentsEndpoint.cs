using System.Threading.Tasks;
using Goodreads.Models.Request;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the comment endpoint of the Goodreads API.
    /// </summary>
    public interface ICommentsEndpoint
    {
        /// <summary>
        /// Get lists comments.
        /// </summary>
        /// <param name="resourceId">Id of resource given as resourceType param.</param>
        /// <param name="type">A resource type.</param>
        /// <param name="page">The desired page from the paginated list of friend requests.</param>
        /// <returns>List of comments.</returns>
        Task<PaginatedList<Comment>> GetAll(long resourceId, ResourceType type, int page = 1);
    }

    public interface IOAuthCommentsEndpoint : ICommentsEndpoint
    {
        /// <summary>
        /// Create a new comment.
        /// </summary>
        /// <param name="resourceId">Id of resource given as resourceType param.</param>
        /// <param name="type">A resource type.</param>
        /// <param name="comment">A comment value.</param>
        /// <returns>True if creation is successed. otherwise false.</returns>
        Task<bool> Create(long resourceId, ResourceType type, string comment);
    }
}
