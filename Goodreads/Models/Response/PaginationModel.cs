using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents pagination information as returned by the Goodreads API
    /// like the page size, current page, etc...
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

        /// <summary>
        /// The size of each page in the paginated list.
        /// </summary>
        public int PageSize
        {
            get
            {
                // As far as I've seen, all pages are 30 items long in the
                // Goodreads API. This might have to change in the future...
                return 30;
            }
        }

        /// <summary>
        /// The current page within the paginated list.
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return (Start / PageSize) + 1;
            }
        }

        /// <summary>
        /// The total number of pages in the paginated list.
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (TotalItems / PageSize) + 1;
            }
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "PaginationModel: CurrentPage: {0}, TotalItems: {1}",
                    CurrentPage,
                    TotalItems);
            }
        }

        internal override void Parse(XElement element)
        {
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
