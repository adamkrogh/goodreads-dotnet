using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Author endpoint of the Goodreads API.
    /// </summary>
    internal sealed class AuthorsEnpoint : Endpoint, IOAuthAuthorsEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorsEnpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public AuthorsEnpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get the author information for the given Id.
        /// </summary>
        /// <param name="authorId">The Goodreads Id for the desired author.</param>
        /// <returns>An async task returning the desired author information.</returns>
        public Task<Author> GetByAuthorId(long authorId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = authorId, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<Author>("author/show.xml", parameters, null, "author");
        }

        /// <summary>
        /// Searches Goodreads for the given name and returns an author id if found, null otherwise.
        /// </summary>
        /// <param name="authorName">The author name to search for.</param>
        /// <returns>A Goodreads author id if found, null otherwise.</returns>
        public async Task<long?> GetAuthorIdByName(string authorName)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "authorName", Value = authorName, Type = ParameterType.UrlSegment }
            };

            // This response is simple enough that we just parse it here without creating another model
            var response = await Connection.ExecuteRaw("api/author_url/{authorName}", parameters).ConfigureAwait(false);
            if (response != null && (int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
            {
                var content = response.Content;
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var document = XDocument.Parse(content);
                    var userElement = document.XPathSelectElement("GoodreadsResponse/author");
                    if (userElement != null)
                    {
                        var attribute = userElement.Attribute("id");
                        if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Value))
                        {
                            return long.Parse(attribute.Value);
                        }
                    }
                }
            }

            return null;
        }
    }
}
