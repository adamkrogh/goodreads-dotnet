using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models an Group book as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class GroupBook : ApiResponse
    {
        /// <summary>
        /// The group book id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The group book updated date.
        /// </summary>
        public DateTime? UpdatedDate { get; private set; }

        /// <summary>
        /// The group book start reading date.
        /// </summary>
        public DateTime? StartReadingDate { get; private set; }

        /// <summary>
        /// The group bok finish reading date.
        /// </summary>
        public DateTime? FinishReadingDate { get; private set; }

        /// <summary>
        /// The book id.
        /// </summary>
        public long BookId { get; private set; }

        /// <summary>
        /// The book title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The book publication year.
        /// </summary>
        public int PublicationYear { get; private set; }

        /// <summary>
        /// The Goodreads author id.
        /// </summary>
        public long AuthorId { get; private set; }

        /// <summary>
        /// The Goodreads author name.
        /// </summary>
        public string AuthorName { get; private set; }

        /// <summary>
        /// The Goodreads author id as user.
        /// </summary>
        public long AuthorUserId { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Group book: BookId: {0}, Title: {1}, Author: {2}",
                    BookId,
                    Title,
                    AuthorName);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            UpdatedDate = element.ElementAsDateTime("updated_at");
            StartReadingDate = element.ElementAsDateTime("start_reading_at");
            FinishReadingDate = element.ElementAsDateTime("finish_reading_at");

            var book = element.Element("book");
            BookId = book.ElementAsLong("id");
            Title = book.ElementAsString("title");
            PublicationYear = book.ElementAsInt("publication_year");

            var author = book.Element("author");
            AuthorId = author.ElementAsLong("id");
            AuthorName = author.ElementAsString("name");
            AuthorUserId = author.ElementAsInt("user_id");
        }
    }
}
