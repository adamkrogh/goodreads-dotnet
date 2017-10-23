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
    public sealed class UserFollowingSummary : ApiResponse
    {
        /// <summary>
        /// The Goodreads User_following Id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The following user id.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// The user id who follows user.
        /// </summary>
        public long FollowerId { get; private set; }

        /// <summary>
        /// The user following created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "UserId: {0}. FollowerId: {1}", UserId, FollowerId);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            UserId = element.ElementAsLong("user-id");
            FollowerId = element.ElementAsLong("follower-id");
            CreatedDateTime = element.ElementAsDateTime("created-at");
        }
    }
}
