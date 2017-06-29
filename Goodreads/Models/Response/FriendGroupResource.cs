using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about an Author following group resource.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class FriendGroupResource : ApiResponse
    {
        /// <summary>
        /// The group resource id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The user id.
        /// </summary>
        public int UserId { get; protected set; }

        /// <summary>
        /// The friend user id.
        /// </summary>
        public int FriendId { get; protected set; }

        /// <summary>
        /// The resource created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; protected set; }

        /// <summary>
        /// The resource updated date.
        /// </summary>
        public DateTime? UpdatedDateTime { get; protected set; }

        /// <summary>
        /// The user approved updated date.
        /// </summary>
        public DateTime? UserApprovedDateTime { get; protected set; }

        /// <summary>
        /// The friend approved date.
        /// </summary>
        public DateTime? FriendApprovedDateTime { get; protected set; }

        /// <summary>
        /// Updates flag.
        /// </summary>
        public bool IsUpdated { get; protected set; }

        /// <summary>
        /// The group resource story.
        /// </summary>
        public string Story { get; protected set; }

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
            Id = element.ElementAsInt("id");
            UserId = element.ElementAsInt("user_id");
            FriendId = element.ElementAsInt("friend_user_id");
            CreatedDateTime = element.ElementAsDateTime("created_at");
            UpdatedDateTime = element.ElementAsDateTime("updated_at");
            UserApprovedDateTime = element.ElementAsDateTime("user_approved_at");
            FriendApprovedDateTime = element.ElementAsDateTime("friend_approved_at");
            IsUpdated = element.ElementAsBool("updates_flag");
            Story = element.ElementAsString("story");
        }
    }
}
