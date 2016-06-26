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
    /// This class models a single book as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Book : ApiResponse
    {
        /// <summary>
        /// The Goodreads Id for this book.
        /// </summary>
        public int Id { get; protected set; }

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
        /// Signifies if this is an eBook or not.
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

        /// <summary>
        /// The aggregate information for this work across all editions of the book.
        /// </summary>
        public Work Work { get; protected set; }

        /// <summary>
        /// The list of authors that worked on this book.
        /// </summary>
        public List<AuthorSummary> Authors { get; protected set; }

        /// <summary>
        /// The most popular shelf names this book appears on. This is a
        /// dictionary of shelf name -> count.
        /// </summary>
        public Dictionary<string, int> PopularShelves { get; protected set; }

        /// <summary>
        /// The list of book links tracked by Goodreads.
        /// This is usually a list of libraries that the user can borrow the book from.
        /// </summary>
        public List<BookLink> BookLinks { get; protected set; }

        /// <summary>
        /// The list of buy links tracked by Goodreads.
        /// This is usually a list of third-party sites that the
        /// user can purchase the book from.
        /// </summary>
        public List<BookLink> BuyLinks { get; protected set; }

        /// <summary>
        /// Summary information about similar books to this one.
        /// </summary>
        public List<BookSummary> SimilarBooks { get; protected set; }

        // TODO: parse series information once I get a better sense
        // of what series are from the other API calls.
        //// public List<Series> Series { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Book: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Title = element.ElementAsString("title");
            Isbn = element.ElementAsString("isbn");
            Isbn13 = element.ElementAsString("isbn13");
            Asin = element.ElementAsString("asin");
            KindleAsin = element.ElementAsString("kindle_asin");
            MarketplaceId = element.ElementAsString("marketplace_id");
            CountryCode = element.ElementAsString("country_code");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            PublicationDate = element.ElementAsGoodreadsDate("publication");
            Publisher = element.ElementAsString("publisher");
            LanguageCode = element.ElementAsString("language_code");
            IsEbook = element.ElementAsBool("is_ebook");
            Description = element.ElementAsString("description");
            AverageRating = element.ElementAsDecimal("average_rating");
            Pages = element.ElementAsInt("num_pages");
            Format = element.ElementAsString("format");
            EditionInformation = element.ElementAsString("edition_information");
            RatingsCount = element.ElementAsInt("ratings_count");
            TextReviewsCount = element.ElementAsInt("text_reviews_count");
            Url = element.ElementAsString("url");

            Work = new Work();
            Work.Parse(element.Element("work"));

            Authors = element.ParseChildren<AuthorSummary>("authors", "author");
            SimilarBooks = element.ParseChildren<BookSummary>("similar_books", "book");

            BookLinks = element.ParseChildren<BookLink>("book_links", "book_link");
            if (BookLinks != null)
            {
                BookLinks.ForEach(x => x.FixBookLink(Id));
            }

            BuyLinks = element.ParseChildren<BookLink>("buy_links", "buy_link");
            if (BuyLinks != null)
            {
                BuyLinks.ForEach(x => x.FixBookLink(Id));
            }

            PopularShelves = element.ParseChildren(
                "popular_shelves",
                "shelf",
                (shelfElement) =>
            {
                var shelfName = shelfElement.Attribute("name").Value;
                var shelfCountValue = shelfElement.Attribute("count").Value;

                int shelfCount = 0;
                int.TryParse(shelfCountValue, out shelfCount);

                return new KeyValuePair<string, int>(shelfName, shelfCount);
            }).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
