using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    public interface ITopicsClient
    {
        /// <summary>
        /// Get info about specified topic.
        /// </summary>
        /// <param name="topicId">A desire topic id.</param>
        /// <returns>A full information about topic identifier.</returns>
        Task<Topic> GetInfo(int topicId);
    }
}
