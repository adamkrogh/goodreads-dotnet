using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Series endpoint of the Goodreads API.
    /// </summary>
    public interface ISeriesClient
    {
        /// <summary>
        /// Get all the series an author has written.
        /// </summary>
        /// <param name="authorId">The author to fetch the list of series for.</param>
        /// <returns>A list of series written by the author.</returns>
        Task<IReadOnlyList<Series>> GetListByAuthorId(int authorId);

        /// <summary>
        /// Get all the series that the given work is in.
        /// </summary>
        /// <param name="workId">The work id to fetch the list of series for.</param>
        /// <returns>A list of series that this work appears in.</returns>
        Task<IReadOnlyList<Series>> GetListByWorkId(int workId);

        /// <summary>
        /// Gets detailed information about the series, including all the works that belong to it.
        /// </summary>
        /// <param name="seriesId">The Goodreads id of the series.</param>
        /// <returns>Information about the series, including a list of works.</returns>
        Task<Series> GetById(int seriesId);
    }
}
