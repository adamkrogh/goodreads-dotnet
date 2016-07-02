using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using RestSharp.Deserializers;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents a collection of statistics for a book in Goodreads.
    /// </summary>
    /// <remarks>
    /// This is one of the few Goodreads endpoints that return JSON for some reason.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReviewStats : ApiResponse
    {
        /// <summary>
        /// The Goodreads Book Id.
        /// </summary>
        [DeserializeAs(Name = "id")]
        public int BookId { get; protected set; }

        /// <summary>
        /// The ISBN of this book.
        /// </summary>
        public string Isbn { get; protected set; }

        /// <summary>
        /// The ISBN13 of this book.
        /// </summary>
        public string Isbn13 { get; protected set; }

        /// <summary>
        /// The number of ratings for this book.
        /// </summary>
        public int RatingsCount { get; protected set; }

        /// <summary>
        /// The number of reviews for this book.
        /// </summary>
        public int ReviewsCount { get; protected set; }

        /// <summary>
        /// The number of text reviews for this book.
        /// </summary>
        public int TextReviewsCount { get; protected set; }

        /// <summary>
        /// The number of ratings across all editions of this book.
        /// </summary>
        public int WorkRatingsCount { get; protected set; }

        /// <summary>
        /// The number of reviews across all editions of this book.
        /// </summary>
        public int WorkReviewsCount { get; protected set; }

        /// <summary>
        /// The number of text reviews across all editions of this book.
        /// </summary>
        public int WorkTextReviewsCount { get; protected set; }

        /// <summary>
        /// The average rating of this book.
        /// </summary>
        public decimal AverageRating { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "ReviewStats: BookId: {0}, AverageRating: {1}",
                    BookId,
                    AverageRating);
            }
        }

        internal override void Parse(XElement element)
        {
            // The review stats are returned in JSON for some reason, no XML parsing needed here.
        }
    }
}
