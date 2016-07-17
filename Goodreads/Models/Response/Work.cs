using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models a work as defined by the Goodreads API.
    /// A work is the root concept of something written. Each book
    /// is a published edition of a piece of work. Most work properties
    /// are aggregate information over all the editions of a work.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Work : ApiResponse
    {
        /// <summary>
        /// The Goodreads Id for this work.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The number of books for this work.
        /// </summary>
        public int BooksCount { get; protected set; }

        /// <summary>
        /// The Goodreads Book Id that is considered the best version of this work.
        /// Might not be populated. See the <see cref="BestBook"/> property for details, if provided.
        /// </summary>
        public int? BestBookId { get; protected set; }

        /// <summary>
        /// The details for the best book of this work. Only populated
        /// if Goodreads provides it as part of the response.
        /// </summary>
        public BestBook BestBook { get; protected set; }

        /// <summary>
        /// If included in a list, this defines this work's position.
        /// </summary>
        public string UserPosition { get; protected set; }

        /// <summary>
        /// The number of reviews of this work.
        /// </summary>
        public int ReviewsCount { get; protected set; }

        /// <summary>
        /// The sum of all ratings of this work.
        /// </summary>
        public int RatingsSum { get; protected set; }

        /// <summary>
        /// The number of ratings of this work.
        /// </summary>
        public int RatingsCount { get; protected set; }

        /// <summary>
        /// The number of text reviews of this work.
        /// </summary>
        public int TextReviewsCount { get; protected set; }

        /// <summary>
        /// The original publication date of this work.
        /// </summary>
        public DateTime? OriginalPublicationDate { get; protected set; }

        /// <summary>
        /// The original title of this work.
        /// </summary>
        public string OriginalTitle { get; protected set; }

        /// <summary>
        /// The original language of this work.
        /// </summary>
        public int? OriginalLanguageId { get; protected set; }

        /// <summary>
        /// The type of media for this work.
        /// </summary>
        public string MediaType { get; protected set; }

        /// <summary>
        /// The distribution of all the ratings for this work.
        /// A dictionary of star rating -> number of ratings.
        /// </summary>
        public IReadOnlyDictionary<int, int> RatingDistribution { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Work: Id: {0}, OriginalTitle: {1}",
                    Id,
                    OriginalTitle);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");

            var bestBookElement = element.Element("best_book");
            if (bestBookElement != null)
            {
                BestBook = new BestBook();
                BestBook.Parse(bestBookElement);
            }

            BestBookId = element.ElementAsNullableInt("best_book_id");
            BooksCount = element.ElementAsInt("books_count");
            ReviewsCount = element.ElementAsInt("reviews_count");
            RatingsSum = element.ElementAsInt("ratings_sum");
            RatingsCount = element.ElementAsInt("ratings_count");
            TextReviewsCount = element.ElementAsInt("text_reviews_count");

            // Merge the Goodreads publication fields into one date property
            var originalPublicationYear = element.ElementAsInt("original_publication_year");
            var originalPublicationMonth = element.ElementAsInt("original_publication_month");
            var originalPublicationDay = element.ElementAsInt("original_publication_day");
            if (originalPublicationYear != 0 && originalPublicationMonth != 0 && originalPublicationDay != 0)
            {
                OriginalPublicationDate = new DateTime(originalPublicationYear, originalPublicationMonth, originalPublicationDay);
            }

            OriginalTitle = element.ElementAsString("original_title");
            OriginalLanguageId = element.ElementAsNullableInt("original_language_id");
            MediaType = element.ElementAsString("media_type");

            // Parse out the rating distribution
            var ratingDistributionElement = element.ElementAsString("rating_dist");
            if (ratingDistributionElement != null)
            {
                var parts = ratingDistributionElement.Split('|');
                if (parts != null && parts.Length > 0)
                {
                    var ratingDistribution = new Dictionary<int, int>();

                    var ratings = parts.Select(x => x.Split(':'))
                                       .Where(x => x[0] != "total")
                                       .OrderBy(x => x[0]);

                    foreach (var rating in ratings)
                    {
                        int star = 0, count = 0;
                        int.TryParse(rating[0], out star);
                        int.TryParse(rating[1], out count);

                        ratingDistribution.Add(star, count);
                    }

                    RatingDistribution = ratingDistribution;
                }
            }
        }

        internal void SetUserPosition(string userPosition)
        {
            UserPosition = userPosition;
        }
    }
}
