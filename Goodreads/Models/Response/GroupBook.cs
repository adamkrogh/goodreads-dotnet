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
    public class GroupBook : ApiResponse
    {
        /// <summary>
        /// The group book id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The group book updated date.
        /// </summary>
        public DateTime? UpdatedDate { get; protected set; }

        /// <summary>
        /// The group book start reading date.
        /// </summary>
        public DateTime? StartReadingDate { get; protected set; }

        /// <summary>
        /// The group bok finish reading date.
        /// </summary>
        public DateTime? FinishReadingDate { get; protected set; }

        /// <summary>
        /// The book id.
        /// </summary>
        public int BookId { get; protected set; }

        /// <summary>
        /// The book title.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The book publication year.
        /// </summary>
        public int PublicationYear { get; protected set; }

        /// <summary>
        /// The Goodreads author id.
        /// </summary>
        public int AuthorId { get; protected set; }

        /// <summary>
        /// The Goodreads author name.
        /// </summary>
        public string AuthorName { get; protected set; }

        /// <summary>
        /// The Goodreads author id as user.
        /// </summary>
        public int AuthorUserId { get; protected set; }

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
            Id = element.ElementAsInt("id");
            UpdatedDate = element.ElementAsDateTime("updated_at");
            StartReadingDate = element.ElementAsDateTime("start_reading_at");
            FinishReadingDate = element.ElementAsDateTime("finish_reading_at");

            var book = element.Element("book");
            BookId = book.ElementAsInt("id");
            Title = book.ElementAsString("title");
            PublicationYear = book.ElementAsInt("publication_year");

            var author = book.Element("author");
            AuthorId = author.ElementAsInt("id");
            AuthorName = author.ElementAsString("name");
            AuthorUserId = author.ElementAsInt("user_id");
        }
    }
}
