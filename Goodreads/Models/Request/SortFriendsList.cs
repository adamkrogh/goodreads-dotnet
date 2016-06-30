using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the Goodreads friends list.
    /// </summary>
    [QueryParameterKey("sort")]
    public enum SortFriendsList
    {
        /// <summary>
        /// Sort by first name.
        /// </summary>
        [QueryParameterValue("first_name")]
        FirstName,

        /// <summary>
        /// Sort by date added.
        /// </summary>
        [QueryParameterValue("date_added")]
        DateAdded,

        /// <summary>
        /// Sort by last online.
        /// </summary>
        [QueryParameterValue("last_online")]
        LastOnline
    }
}
