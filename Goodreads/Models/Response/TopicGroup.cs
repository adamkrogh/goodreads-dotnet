using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models an topic group as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class TopicGroup : ApiResponse
    {
        /// <summary>
        /// Folder id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The folder name.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Topic group image url.
        /// </summary>
        public string ImageUrl { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Folder: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Title = element.ElementAsString("title");
            ImageUrl = element.ElementAsString("p1_image_url");
        }
    }
}
