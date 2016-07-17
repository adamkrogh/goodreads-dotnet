using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents information about a book series as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Series : ApiResponse
    {
        /// <summary>
        /// The Id of the series.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The title of the series.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The description of the series.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Any notes for the series.
        /// </summary>
        public string Note { get; protected set; }

        /// <summary>
        /// How many works are contained in the series total.
        /// </summary>
        public int SeriesWorksCount { get; protected set; }

        /// <summary>
        /// The count of works that are considered primary in the series.
        /// </summary>
        public int PrimaryWorksCount { get; protected set; }

        /// <summary>
        /// Determines if the series is usually numbered or not.
        /// </summary>
        public bool IsNumbered { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Series: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Title = element.ElementAsString("title", true);
            Description = element.ElementAsString("description", true);
            Note = element.ElementAsString("note", true);
            SeriesWorksCount = element.ElementAsInt("series_works_count");
            PrimaryWorksCount = element.ElementAsInt("primary_work_count");
            IsNumbered = element.ElementAsBool("numbered");
        }
    }
}
