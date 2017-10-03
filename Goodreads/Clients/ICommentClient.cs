using Goodreads.Models.Request;
using System.Threading.Tasks;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the comment endpoint of the Goodreads API.
    /// </summary>
    public interface ICommentClient
    {
        /// <summary>
        /// Create a new comment.
        /// </summary>
        /// <param name="resourceId">Id of resource given as resourceType param.</param>
        /// <param name="type">A resource type.</param>
        /// <param name="comment">A comment value.</param>
        /// <returns>True if creation is successed. otherwise false.</returns>
        Task<bool> Create(int resourceId, ResourceType type, string comment);
    }
}
