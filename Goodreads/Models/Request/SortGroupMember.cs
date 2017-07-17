using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the Goodreads group members.
    /// </summary>
    [QueryParameterKey("sort")]
    public enum SortGroupMember
    {
        /// <summary>
        /// Sort by the last online activity.
        /// </summary>
        [QueryParameterValue("last_online'")]
        LastOnline,

        /// <summary>
        /// Sort by numbers of comments.
        /// </summary>
        [QueryParameterValue("num_comments")]
        NumberComments,

        /// <summary>
        /// Sort by joined date.
        /// </summary>
        [QueryParameterValue("date_joined")]
        JoinedDate,

        /// <summary>
        /// Sort by number of books.
        /// </summary>
        [QueryParameterValue("num_books")]
        NumberBooks,

        /// <summary>
        /// Sort by first name.
        /// </summary>
        [QueryParameterValue("first_name")]
        FristName
    }
}
