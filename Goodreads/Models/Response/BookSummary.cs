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
    /// This class models areas of the API where Goodreads returns
    /// very brief information about a Book instead of their entire object.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BookSummary : ApiResponse
    {
        /// <summary>
        /// The Id of this book.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The title of this book.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The title of this book without series information in it.
        /// </summary>
        public string TitleWithoutSeries { get; protected set; }

        /// <summary>
        /// The link to the Goodreads page for this book.
        /// </summary>
        public string Link { get; protected set; }

        /// <summary>
        /// The cover image of this book, regular size.
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// The cover image of this book, small size.
        /// </summary>
        public string SmallImageUrl { get; protected set; }

        /// <summary>
        /// The work id of this book.
        /// </summary>
        public int? WorkId { get; protected set; }

        /// <summary>
        /// The ISBN of this book.
        /// </summary>
        public string Isbn { get; protected set; }

        /// <summary>
        /// The ISBN13 of this book.
        /// </summary>
        public string Isbn13 { get; protected set; }

        /// <summary>
        /// The average rating of the book.
        /// </summary>
        public decimal? AverageRating { get; protected set; }

        /// <summary>
        /// The count of all ratings for the book.
        /// </summary>
        public int? RatingsCount { get; protected set; }

        /// <summary>
        /// The date this book was published.
        /// </summary>
        public DateTime? PublicationDate { get; protected set; }

        /// <summary>
        /// Summary information about the authors of this book.
        /// </summary>
        public IReadOnlyList<AuthorSummary> Authors { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "BookSummary: BookId: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Title = element.ElementAsString("title");
            TitleWithoutSeries = element.ElementAsString("title_without_series");
            Link = element.ElementAsString("link");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            Isbn = element.ElementAsString("isbn");
            Isbn13 = element.ElementAsString("isbn13");
            AverageRating = element.ElementAsNullableDecimal("average_rating");
            RatingsCount = element.ElementAsNullableInt("ratings_count");
            PublicationDate = element.ElementAsMultiDateField("publication");
            Authors = element.ParseChildren<AuthorSummary>("authors", "author");

            var workElement = element.Element("work");
            if (workElement != null)
            {
                WorkId = workElement.ElementAsNullableInt("id");
            }
        }
    }
}
