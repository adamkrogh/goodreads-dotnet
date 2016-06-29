using System.ComponentModel;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the Goodreads friends list.
    /// </summary>
    public enum SortFriendsList
    {
        /// <summary>
        /// Sort by first name.
        /// </summary>
        [Description("first_name")]
        FirstName,

        /// <summary>
        /// Sort by date added.
        /// </summary>
        [Description("date_added")]
        DateAdded,

        /// <summary>
        /// Sort by last online.
        /// </summary>
        [Description("last_online")]
        LastOnline
    }
}
