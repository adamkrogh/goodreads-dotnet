using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the Goodreads group folder list.
    /// </summary>
    [QueryParameterKey("sort")]
    public enum GroupFolderSort
    {
        /// <summary>
        /// Sort by comments count.
        /// </summary>
        [QueryParameterValue("comments_count")]
        CommentsCount,

        /// <summary>
        /// Sort by title.
        /// </summary>
        [QueryParameterValue("title")]
        Title,

        /// <summary>
        /// Sort by updated at.
        /// </summary>
        [QueryParameterValue("updated_at")]
        UpdatedAt,

        /// <summary>
        /// Sort by views.
        /// </summary>
        [QueryParameterValue("views")]
        Views
    }
}
