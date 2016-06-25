using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// very brief information about an Author instead of their entire profile.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuthorSummary : ApiResponse
    {
        /// <summary>
        /// The Goodreads Author Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The name of this author.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The role of this author.
        /// </summary>
        public string Role { get; protected set; }

        /// <summary>
        /// The image of this author, regular size.
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// The image of this author, small size.
        /// </summary>
        public string SmallImageUrl { get; protected set; }

        /// <summary>
        /// The link to the Goodreads page for this author.
        /// </summary>
        public string Link { get; protected set; }

        /// <summary>
        /// The average rating for all of this author's books.
        /// </summary>
        public decimal? AverageRating { get; protected set; }

        /// <summary>
        /// The total count of all ratings of this author's books.
        /// </summary>
        public int? RatingsCount { get; protected set; }

        /// <summary>
        /// The total count of all the text reviews of this author's books.
        /// </summary>
        public int? TextReviewsCount { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "AuthorSummary: Id: {0}, Name: {1}",
                    Id,
                    Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Name = element.ElementAsString("name");
            Role = element.ElementAsString("role");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            Link = element.ElementAsString("link");
            AverageRating = element.ElementAsNullableDecimal("average_rating");
            RatingsCount = element.ElementAsNullableInt("ratings_count");
            TextReviewsCount = element.ElementAsNullableInt("text_reviews_count");
        }
    }
}
