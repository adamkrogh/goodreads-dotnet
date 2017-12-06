using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The update type.
    /// </summary>
    [QueryParameterKey("topic[subject_type]")]
    public enum TopicSubjectType
    {
        /// <summary>
        /// Book subject type.
        /// </summary>
        [QueryParameterValue("Book")]
        Book,

        /// <summary>
        /// Group subject type.
        /// </summary>
        [QueryParameterValue("Group")]
        Group
    }
}
