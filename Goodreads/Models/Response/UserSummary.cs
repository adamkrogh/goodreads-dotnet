using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents summary information about a user as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UserSummary : ApiResponse
    {
        /// <summary>
        /// The Id of the user.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The link to the user's Goodreads profile page.
        /// </summary>
        public string Link { get; protected set; }

        /// <summary>
        /// The profile image for the user, regular size.
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// THe profile image for the user, small size.
        /// </summary>
        public string SmallImageUrl { get; protected set; }

        /// <summary>
        /// The number of friends the user has on Goodreads.
        /// </summary>
        public int FriendsCount { get; protected set; }

        /// <summary>
        /// The number of reviews the user has made on Goodreads.
        /// </summary>
        public int ReviewsCount { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "UserSummary: Id: {0}, Name: {1}",
                    Id,
                    Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Name = element.ElementAsString("name");
            Link = element.ElementAsString("link");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            FriendsCount = element.ElementAsInt("friends_count");
            ReviewsCount = element.ElementAsInt("reviews_count");
        }
    }
}
