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
    public sealed class UserFollowers : ApiResponse
    {
        /// <summary>
        /// The follower unique identifier.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The follower name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The follower link.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The follower image url.
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The follower small image.
        /// </summary>
        public string SmallImageUrl { get; private set; }

        /// <summary>
        /// The follower friends count.
        /// </summary>
        public int FriendsCount { get; private set; }

        /// <summary>
        /// The follower review count.
        /// </summary>
        public int ReviewsCount { get; private set; }

        /// <summary>
        /// Determine whether the follower is user friend.
        /// </summary>
        public bool? IsMutualFriend { get; private set; }

        /// <summary>
        /// The created date.
        /// </summary>
        public DateTime? CreatedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "User: {0}. Name: {1}", Id, Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Name = element.ElementAsString("name");
            Link = element.ElementAsString("link");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            FriendsCount = element.ElementAsInt("friends_count");
            ReviewsCount = element.ElementAsInt("reviews_count");
            IsMutualFriend = element.ElementAsBool("is_mutual_friend");
            CreatedAt = element.ElementAsDateTime("created_at");
        }
    }
}
