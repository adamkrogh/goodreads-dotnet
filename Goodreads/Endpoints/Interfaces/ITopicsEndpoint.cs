using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
	/// <summary>
    /// API client for the Topic endpoint.
    /// </summary>
    public interface ITopicsEndpoint
    {        
    }

	// <summary>
    /// API OAuth client for the Shelves endpoint.
    /// </summary>
    public interface IOAuthTopicsEndpoint : ITopicsEndpoint
    {
		/// <summary>
        /// Get info about specified topic.
        /// </summary>
        /// <param name="topicId">A desire topic id.</param>
        /// <returns>A full information about topic identifier.</returns>
        Task<Topic> GetInfo(long topicId);
    }
}
