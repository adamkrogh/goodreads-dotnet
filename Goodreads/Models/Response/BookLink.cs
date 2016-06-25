using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models a book link as defined by the Goodreads API.
    /// This is usually a link to a third-party site to purchase the book.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BookLink : ApiResponse
    {
        /// <summary>
        /// The Id of this book link.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The name of this book link provider.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The link to this book on the provider's site.
        /// Be sure to append book_id as a query parameter
        /// to actually be redirected to the correct page.
        /// </summary>
        public string Link { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "BookLink: Id: {0}, Name: {1}",
                    Id,
                    Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Name = element.ElementAsString("name");
            Link = element.ElementAsString("link");
        }

        /// <summary>
        /// Goodreads returns incomplete book links for some reason.
        /// The link results in an error unless you append a book_id query parameter.
        /// This method fixes up these book links with the given book id.
        /// </summary>
        /// <param name="bookId">The book id to append to the book link.</param>
        internal void FixBookLink(int bookId)
        {
            if (!string.IsNullOrWhiteSpace(Link))
            {
                if (!Link.Contains("book_id"))
                {
                    Link += (Link.Contains("?") ? "&" : "?") + "book_id=" + bookId;
                }
            }
        }
    }
}
