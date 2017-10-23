using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about an user owned books.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class OwnedBook : ApiResponse
    {
        /// <summary>
        /// The owner book id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The owner id.
        /// </summary>
        public long OwnerId { get; private set; }

        /// <summary>
        /// The original date when owner has bought a book.
        /// </summary>
        public DateTime? OriginalPurchaseDate { get; private set; }

        /// <summary>
        /// The original location where owner has bought a book.
        /// </summary>
        public string OriginalPurchaseLocation { get; private set; }

        /// <summary>
        /// The owned book condition.
        /// </summary>
        public string Condition { get; private set; }

        /// <summary>
        /// The traded count.
        /// </summary>
        public int TradedCount { get; private set; }

        /// <summary>
        /// The link to the owned book.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The book.
        /// </summary>
        public BookSummary Book { get; private set; }

        /// <summary>
        /// The owned book review.
        /// </summary>
        public Review Review { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "OwnerId: {0}; Book: {1}",
                    OwnerId,
                    Book?.Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            OwnerId = element.ElementAsLong("current_owner_id");
            OriginalPurchaseDate = element.ElementAsDateTime("original_purchase_date");
            OriginalPurchaseLocation = element.ElementAsString("original_purchase_location");
            Condition = element.ElementAsString("condition");

            var review = element.Element("review");
            if (review != null)
            {
                Review = new Review();
                Review.Parse(review);
            }

            var book = element.Element("book");
            if (book != null)
            {
                Book = new BookSummary();
                Book.Parse(book);
            }
        }
    }
}
