using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models a single book as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Book : ApiResponse
    {
        /// <summary>
        /// The Goodreads Id for this book.
        /// </summary>
        public string Id { get; protected set; }

        /// <summary>
        /// The title of this book.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The description of this book.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The ISBN of this book.
        /// </summary>
        public string Isbn { get; protected set; }

        /// <summary>
        /// The ISBN13 of this book.
        /// </summary>
        public string Isbn13 { get; protected set; }

        /// <summary>
        /// The ASIN of this book.
        /// </summary>
        public string Asin { get; protected set; }

        /// <summary>
        /// The Kindle ASIN of this book.
        /// </summary>
        public string KindleAsin { get; protected set; }

        /// <summary>
        /// The marketplace Id of this book.
        /// </summary>
        public string MarketplaceId { get; protected set; }

        /// <summary>
        /// The country code of this book.
        /// </summary>
        public string CountryCode { get; protected set; }

        /// <summary>
        /// The cover image for this book.
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// The small cover image for this book.
        /// </summary>
        public string SmallImageUrl { get; protected set; }

        /// <summary>
        /// The date this book was published.
        /// </summary>
        public DateTime? PublicationDate { get; protected set; }

        /// <summary>
        /// The publisher of this book.
        /// </summary>
        public string Publisher { get; protected set; }

        /// <summary>
        /// The language code of this book.
        /// </summary>
        public string LanguageCode { get; protected set; }

        /// <summary>
        /// Signifies if this is an EBook or now.
        /// </summary>
        public bool IsEbook { get; protected set; }

        /// <summary>
        /// The average rating of this book by Goodreads users.
        /// </summary>
        public decimal AverageRating { get; protected set; }

        /// <summary>
        /// The number of pages in this book.
        /// </summary>
        public int Pages { get; protected set; }

        /// <summary>
        /// The format of this book.
        /// </summary>
        public string Format { get; protected set; }

        /// <summary>
        /// Brief information about this edition of the book.
        /// </summary>
        public string EditionInformation { get; protected set; }

        /// <summary>
        /// The count of all Goodreads ratings for this book.
        /// </summary>
        public int RatingsCount { get; protected set; }

        /// <summary>
        /// The count of all reviews that contain text for this book.
        /// </summary>
        public int TextReviewsCount { get; protected set; }

        /// <summary>
        /// The Goodreads Url for this book.
        /// </summary>
        public string Url { get; protected set; }

        internal override void Parse(XElement element)
        {
            // TODO: parse XML and populate Book model
        }
    }
}
