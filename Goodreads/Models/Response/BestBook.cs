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
    public class BestBook : ApiResponse
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
        /// The Goodreads id of the author.
        /// </summary>
        public int AuthorId { get; protected set; }

        /// <summary>
        /// The name of the author.
        /// </summary>
        public string AuthorName { get; protected set; }

        /// <summary>
        /// The cover image of this book.
        /// </summary>
        public string ImageUrl { get; protected set; }

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
            Id = element.ElementAsInt("id");
            Title = element.ElementAsString("title");

            var authorElement = element.Element("author");
            if (authorElement != null)
            {
                AuthorId = authorElement.ElementAsInt("id");
                AuthorName = authorElement.ElementAsString("name");
            }

            ImageUrl = element.ElementAsString("image_url");
        }
    }
}
