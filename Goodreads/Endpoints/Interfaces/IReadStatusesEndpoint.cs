using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the read statuses endpoint.
    /// </summary>
    public interface IReadStatusesEndpoint
    {
        /// <summary>
        /// Get information about a read status update.
        /// </summary>
        /// <param name="id">A desire read status unique identifier.</param>
        /// <returns>A full information about desire read status.</returns>
        Task<ReadStatus> GetReadStatus(long id);
    }

    /// <summary>
    /// API OAuth client for the read statuses endpoint.
    /// </summary>
    public interface IOAuthReadStatusesEndpoint : IReadStatusesEndpoint
    {
    }
}
