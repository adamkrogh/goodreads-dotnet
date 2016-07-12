using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents pagination information as returned by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PaginationModel : ApiResponse
    {
        /// <summary>
        /// The item the current page starts on.
        /// </summary>
        public int Start { get; protected set; }

        /// <summary>
        /// The item the current page ends on.
        /// </summary>
        public int End { get; protected set; }

        /// <summary>
        /// The total number of items in the paginated list.
        /// </summary>
        public int TotalItems { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "PaginationModel: {0}-{1} of {2}",
                    Start,
                    End,
                    TotalItems);
            }
        }

        internal override void Parse(XElement element)
        {
            // Search results have different pagination fields for some reason...
            if (element.Name == "search")
            {
                Start = element.ElementAsInt("results-start");
                End = element.ElementAsInt("results-end");
                TotalItems = element.ElementAsInt("total-results");
                return;
            }

            var startAttribute = element.Attribute("start");
            var endAttribute = element.Attribute("end");
            var totalAttribute = element.Attribute("total");

            if (startAttribute != null &&
                endAttribute != null &&
                totalAttribute != null)
            {
                int start = 0, end = 0, total = 0;
                int.TryParse(startAttribute.Value, out start);
                int.TryParse(endAttribute.Value, out end);
                int.TryParse(totalAttribute.Value, out total);

                Start = start;
                End = end;
                TotalItems = total;
            }
        }
    }
}
