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
    internal sealed class SeriesEndpoint : Endpoint, IOAuthSeriesEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public SeriesEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get all the series an author has written.
        /// </summary>
        /// <param name="authorId">The author to fetch the list of series for.</param>
        /// <returns>A list of series written by the author.</returns>
        public async Task<IReadOnlyList<Series>> GetListByAuthorId(long authorId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = authorId, Type = ParameterType.QueryString }
            };

            try
            {
                var response = await Connection.ExecuteRaw("series/list", parameters).ConfigureAwait(false);
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

        /// <summary>
        /// Get all the series that the given work is in.
        /// </summary>
        /// <param name="workId">The work id to fetch the list of series for.</param>
        /// <returns>A list of series that this work appears in.</returns>
        public async Task<IReadOnlyList<Series>> GetListByWorkId(long workId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = workId, Type = ParameterType.UrlSegment }
            };

            try
            {
                var response = await Connection.ExecuteRaw("series/work/{id}", parameters).ConfigureAwait(false);
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

                            return seriesModels;
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

        /// <summary>
        /// Gets detailed information about the series, including all the works that belong to it.
        /// </summary>
        /// <param name="seriesId">The Goodreads id of the series.</param>
        /// <returns>Information about the series, including a list of works.</returns>
        public async Task<Series> GetById(long seriesId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = seriesId, Type = ParameterType.UrlSegment }
            };

            return await Connection.ExecuteRequest<Series>("series/show/{id}", parameters, null, "series").ConfigureAwait(false);
        }
    }
}
