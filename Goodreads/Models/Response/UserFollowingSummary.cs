using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// summary information about an User following.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UserFollowingSummary : ApiResponse
    {
        /// <summary>
        /// The Goodreads User_following Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The following user id.
        /// </summary>
        public int UserId { get; protected set; }

        /// <summary>
        /// The user id who follows user.
        /// </summary>
        public int FollowerId { get; protected set; }

        /// <summary>
        /// The user following created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "UserId: {0}. FollowerId: {1}", UserId, FollowerId);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            UserId = element.ElementAsInt("user-id");
            FollowerId = element.ElementAsInt("follower-id");
            CreatedDateTime = element.ElementAsDateTime("created-at");
        }
    }
}
