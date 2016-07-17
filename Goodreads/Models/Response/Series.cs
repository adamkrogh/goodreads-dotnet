using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

        /// <summary>
        /// The list of works that are in this series.
        /// Only populated if Goodreads returns it in the response.
        /// </summary>
        public IReadOnlyList<Work> Works { get; protected set; }

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

            var seriesWorksElement = element.Element("series_works");
            if (seriesWorksElement != null)
            {
                ParseSeriesWorks(seriesWorksElement);
            }
        }

        /// <summary>
        /// In order to make out API models less complicated than the mess that Goodreads responds with,
        /// we merge the concept of "series works" and "works" by copying the only
        /// useful piece of information (user position) to the work object.
        /// </summary>
        /// <param name="seriesWorksRootElement">The root element of the list of series works.</param>
        private void ParseSeriesWorks(XElement seriesWorksRootElement)
        {
            var seriesWorkElements = seriesWorksRootElement.Descendants("series_work");
            if (seriesWorkElements != null && seriesWorkElements.Count() > 0)
            {
                var works = new List<Work>();
                foreach (var seriesWorkElement in seriesWorkElements)
                {
                    var userPosition = seriesWorkElement.ElementAsString("user_position");

                    var workElement = seriesWorkElement.Element("work");
                    if (workElement != null)
                    {
                        var work = new Work();
                        work.Parse(workElement);
                        work.SetUserPosition(userPosition);
                        works.Add(work);
                    }
                }

                Works = works;
            }
        }
    }
}
