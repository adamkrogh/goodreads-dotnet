using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// summary information about an user owned books.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class OwnedBookSummary : ApiResponse
    {
        /// <summary>
        /// The owner book id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The current owner id.
        /// </summary>
        public long CurrentOwnerId { get; private set; }

        /// <summary>
        /// The current owner name.
        /// </summary>
        public string CurrentOwnerName { get; private set; }

        /// <summary>
        /// The original date when owner has bought a book.
        /// </summary>
        public DateTime? OriginalPurchaseDate { get; private set; }

        /// <summary>
        /// The original location where owner has bought a book.
        /// </summary>
        public string OriginalPurchaseLocation { get; private set; }

        /// <summary>
        /// The owned book condition code.
        /// </summary>
        public int ConditionCode { get; private set; }

        /// <summary>
        /// The owned book condition description.
        /// </summary>
        public string ConditionDescription { get; private set; }

        /// <summary>
        /// The traded count.
        /// </summary>
        public int TradedCount { get; private set; }

        /// <summary>
        /// The owned book unique code.
        /// </summary>
        public int? UniqueCode { get; private set; }

        /// <summary>
        /// The book id.
        /// </summary>
        public long BookId { get; private set; }

        /// <summary>
        /// The updated date of current owned book.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// The created date of current owned book.
        /// </summary>
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// Determine whether book is available for swap.
        /// </summary>
        public bool AvailableForSwap { get; private set; }

        /// <summary>
        /// Determine whether book is already swappable.
        /// </summary>
        public bool IsSwappable { get; private set; }

        /// <summary>
        /// The comments count for the current owned book.
        /// </summary>
        public int CommentsCount { get; private set; }

        /// <summary>
        /// The date of the last comment for the current owned book.
        /// </summary>
        public DateTime? LastCommentAt { get; private set; }

        /// <summary>
        /// The work id of the current owned book.
        /// </summary>
        public long WorkId { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "OwnerId: {0}; Book: {1}",
                    CurrentOwnerId,
                    BookId);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            CurrentOwnerId = element.ElementAsLong("current-owner-id");
            CurrentOwnerName = element.ElementAsString("current-owner-name");
            OriginalPurchaseDate = element.ElementAsDateTime("original-purchase-date");
            OriginalPurchaseLocation = element.ElementAsString("original-purchase-location");
            ConditionCode = element.ElementAsInt("condition-code");
            ConditionDescription = element.ElementAsString("condition-description");
            BookId = element.ElementAsLong("book-id");
            WorkId = element.ElementAsLong("work-id");
            AvailableForSwap = element.ElementAsBool("available-for-swap");
            IsSwappable = element.ElementAsBool("swappable-flag");
            CommentsCount = element.ElementAsInt("comments-count");
            LastCommentAt = element.ElementAsDateTime("last-comment-at");
            UniqueCode = element.ElementAsInt("unique-code");
            CreatedAt = element.ElementAsDateTime("created-at");
            UpdatedAt = element.ElementAsDateTime("updated-at");
            TradedCount = element.ElementAsInt("book-trades-count");
        }
    }
}
