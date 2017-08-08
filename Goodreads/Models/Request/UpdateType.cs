using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The update type.
    /// </summary>
    [QueryParameterKey("update")]
    public enum UpdateType
    {
        /// <summary>
        /// Books update type.
        /// </summary>
        [QueryParameterValue("books")]
        Books,

        /// <summary>
        /// Reviews update type.
        /// </summary>
        [QueryParameterValue("reviews")]
        Reviews,

        /// <summary>
        /// Statuses update type.
        /// </summary>
        [QueryParameterValue("statuses")]
        Statuses
    }
}
