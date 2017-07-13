using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the Goodreads group info.
    /// </summary>
    [QueryParameterKey("order")]
    public enum OrderGroupInfo
    {
        /// <summary>
        /// Order by ascending.
        /// </summary>
        [QueryParameterValue("a")]
        Asc,

        /// <summary>
        /// Order by descending.
        /// </summary>
        [QueryParameterValue("d")]
        Desc
    }
}
