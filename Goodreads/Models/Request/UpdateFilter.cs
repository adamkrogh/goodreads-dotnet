using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The update filter.
    /// </summary>
    [QueryParameterKey("update_filter")]
    public enum UpdateFilter
    {
        /// <summary>
        /// Filter updates by friends.
        /// </summary>
        [QueryParameterValue("friends ")]
        Friends,

        /// <summary>
        /// Filter updates by followers.
        /// </summary>
        [QueryParameterValue("following")]
        Following,

        /// <summary>
        /// Filter updates by top friends.
        /// </summary>
        [QueryParameterValue("top_friends")]
        TopFriends
    }
}
