using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about a friend group resource.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class FriendGroupResource : ApiResponse
    {
        /// <summary>
        /// The group resource id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The user id.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// The friend user id.
        /// </summary>
        public long FriendId { get; private set; }

        /// <summary>
        /// The resource created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; private set; }

        /// <summary>
        /// The resource updated date.
        /// </summary>
        public DateTime? UpdatedDateTime { get; private set; }

        /// <summary>
        /// The user approved updated date.
        /// </summary>
        public DateTime? UserApprovedDateTime { get; private set; }

        /// <summary>
        /// The friend approved date.
        /// </summary>
        public DateTime? FriendApprovedDateTime { get; private set; }

        /// <summary>
        /// Updates flag.
        /// </summary>
        public bool IsUpdated { get; private set; }

        /// <summary>
        /// The group resource story.
        /// </summary>
        public string Story { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}. UserId: {1}. FriendId: {2}",
                    Id,
                    UserId,
                    FriendId);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            UserId = element.ElementAsLong("user_id");
            FriendId = element.ElementAsLong("friend_user_id");
            CreatedDateTime = element.ElementAsDateTime("created_at");
            UpdatedDateTime = element.ElementAsDateTime("updated_at");
            UserApprovedDateTime = element.ElementAsDateTime("user_approved_at");
            FriendApprovedDateTime = element.ElementAsDateTime("friend_approved_at");
            IsUpdated = element.ElementAsBool("updates_flag");
            Story = element.ElementAsString("story");
        }
    }
}
