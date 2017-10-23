using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models the best book in a work, as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class BestBook : ApiResponse
    {
        /// <summary>
        /// The Id of this book.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The title of this book.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The Goodreads id of the author.
        /// </summary>
        public long AuthorId { get; private set; }

        /// <summary>
        /// The name of the author.
        /// </summary>
        public string AuthorName { get; private set; }

        /// <summary>
        /// The cover image of this book.
        /// </summary>
        public string ImageUrl { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "BestBook: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Title = element.ElementAsString("title");

            var authorElement = element.Element("author");
            if (authorElement != null)
            {
                AuthorId = authorElement.ElementAsLong("id");
                AuthorName = authorElement.ElementAsString("name");
            }

            ImageUrl = element.ElementAsString("image_url");
        }
    }
}
