using System.Threading.Tasks;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the read statuses endpoint.
    /// </summary>
    public interface IReadStatusesClient
    {
        /// <summary>
        /// Get information about a read status update.
        /// </summary>
        /// <param name="id">A desire read status unique identifier.</param>
        /// <returns>A full information about desire read status.</returns>
        Task<ReadStatus> GetReadStatus(int id);
    }
}
