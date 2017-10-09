using System.Threading.Tasks;
using Goodreads.Models.Request;

namespace Goodreads.Clients
{
    public interface IRatingClient
    {
        /// <summary>
        /// Like a resource.
        /// </summary>
        /// <param name="resourceId">A desire resource identifier.</param>
        /// <param name="type">A desire resource type.</param>
        /// <returns>True if liking is scucessed, otherwise false.</returns>
        Task<bool> LikeResource(int resourceId, ResourceType type);
    }
}
