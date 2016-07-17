using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Series endpoint of the Goodreads API.
    /// </summary>
    public class SeriesClient : ISeriesClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public SeriesClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Get all the series an author has written.
        /// </summary>
        /// <param name="authorId">The author to fetch the list of series for.</param>
        /// <returns>A list of series written by the author.</returns>
        public async Task<IReadOnlyList<Series>> GetListByAuthorId(int authorId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = authorId, Type = ParameterType.QueryString }
            };

            try
            {
                var response = await Connection.ExecuteRaw("series/list", parameters);
                if (response != null && (int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                {
                    var content = response.Content;
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        var document = XDocument.Parse(content);
                        var series = document.XPathSelectElements("GoodreadsResponse/series_works/series_work/series");
                        if (series != null && series.Count() > 0)
                        {
                            var seriesModels = new List<Series>();
                            foreach (var seriesElement in series)
                            {
                                var seriesModel = new Series();
                                seriesModel.Parse(seriesElement);
                                seriesModels.Add(seriesModel);
                            }

                            // Goodreads returns way too many duplicates, group by them by id first.
                            var grouped = seriesModels.GroupBy(x => x.Id);
                            var uniqueSeries = grouped.Select(x => x.First()).ToList();

                            return uniqueSeries;
                        }
                    }
                }
            }
            catch
            {
                // Just ignore the error and return null...
            }

            return null;
        }
    }
}
