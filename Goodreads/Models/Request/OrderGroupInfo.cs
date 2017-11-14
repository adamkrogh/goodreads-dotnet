using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the Goodreads info
    /// </summary>
    [QueryParameterKey("order")]
    public enum OrderInfo
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
