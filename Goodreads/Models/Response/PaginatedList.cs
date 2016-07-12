using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents a paginated list of objects as returned by the Goodreads API,
    /// along with pagination information about the page size, current page, etc...
    /// </summary>
    /// <typeparam name="T">The type of the object in the paginated list.</typeparam>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PaginatedList<T> : ApiResponse where T : ApiResponse, new()
    {
        /// <summary>
        /// The list of objects for the current page.
        /// </summary>
        public IReadOnlyList<T> List { get; protected set; }

        /// <summary>
        /// Pagination information about the list and current page.
        /// </summary>
        public PaginationModel Pagination { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "PaginatedList: CurrentItems: {0}, TotalItems: {1}",
                    List == null ? 0 : List.Count,
                    Pagination == null ? 0 : Pagination.TotalItems);
            }
        }

        internal override void Parse(XElement element)
        {
            Pagination = new PaginationModel();
            Pagination.Parse(element);

            // Should have known search pagination would be different...
            if (element.Name == "search")
            {
                var results = element.Descendants("results");
                if (results != null && results.Count() == 1)
                {
                    List = results.First().ParseChildren<T>();
                }
            }
            else
            {
                List = element.ParseChildren<T>();
            }
        }
    }
}
