using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about an User following.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class UserFollowing : ApiResponse
    {
        /// <summary>
        /// The following user id.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// The following user name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The following user link.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The profile image for the following user, regular size.
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The profile image for the following user, small size.
        /// </summary>
        public string SmallImageUrl { get; private set; }

        /// <summary>
        /// The following user friends count.
        /// </summary>
        public int FriendsCount { get; private set; }

        /// <summary>
        /// The following user reviews count.
        /// </summary>
        public int ReviewsCount { get; private set; }

        /// <summary>
        /// Is the following user a mutual friend.
        /// </summary>
        public bool IsMutualFriend { get; private set; }

        /// <summary>
        /// The following status.
        /// </summary>
        public UserFollowingStatus UserStatus { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "User: {0}. Name: {1}", UserId, Name);
            }
        }

        internal override void Parse(XElement element)
        {
            UserId = element.ElementAsLong("id");
            Name = element.ElementAsString("name");
            Link = element.ElementAsString("link");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            FriendsCount = element.ElementAsInt("friends_count");
            ReviewsCount = element.ElementAsInt("reviews_count");
            IsMutualFriend = element.ElementAsBool("is_mutual_friend");

            var status = element.Element("user_status");
            if (status != null)
            {
                UserStatus = new UserFollowingStatus();
                UserStatus.Parse(status);
            }
        }
    }
}
