using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class CompareBook : ApiResponse
    {
        /// <summary>
        /// The book id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The book title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The book url.
        /// </summary>
        public string Url { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}. Title: {1}.",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Title = element.ElementAsString("title");
            Url = element.ElementAsString("url");
        }
    }
}
