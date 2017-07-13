using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the Goodreads group info.
    /// </summary>
    [QueryParameterKey("sort")]
    public enum SortGroupInfo
    {
        /// <summary>
        /// Sort by comments count.
        /// </summary>
        [QueryParameterValue("comments_count'")]
        CommentsCount,

        /// <summary>
        /// Sort by updated date.
        /// </summary>
        [QueryParameterValue("updated_at")]
        UpdatedAt,

        /// <summary>
        /// Sort by views.
        /// </summary>
        [QueryParameterValue("views")]
        Views,

        /// <summary>
        /// Sort by title.
        /// </summary>
        [QueryParameterValue("title")]
        Title
    }
}
