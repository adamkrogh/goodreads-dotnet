using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class ShortReview : ApiResponse
    {
        /// <summary>
        /// The review id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The review rating.
        /// </summary>
        public string Rating { get; private set; }

        /// <summary>
        /// The review text.
        /// </summary>
        public string Text { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}. Rating: {1}. Text: {2}",
                    Id,
                    Rating,
                    Text);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Rating = element.ElementAsString("rating");
            Text = element.ElementAsString("text");
        }
    }
}
