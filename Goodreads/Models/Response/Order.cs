using Goodreads.Http;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Determines the sort order of resources in the Goodreads API.
    /// </summary>
    [QueryParameterKey("order")]
    public enum Order
    {
        /// <summary>
        /// Ascending sort order.
        /// </summary>
        [QueryParameterValue("a")]
        Ascending = 0,

        /// <summary>
        /// Descending sort order.
        /// </summary>
        [QueryParameterValue("d")]
        Descending
    }
}
