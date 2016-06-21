using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goodreads.Clients
{
    public class AuthorsClient : IAuthorsClient
    {
        private readonly IConnection _connection;

        public AuthorsClient(IConnection connection)
        {
            _connection = connection;
        }

        #region IAuthorsClient Members
        
        /// <summary>
        /// Retrieve a single author
        /// </summary>
        public Task<Author> Get(int authorId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = authorId, Type = ParameterType.UrlSegment }
            };
            return _connection.ExecuteRequest<Author>("author/list/{id}", parameters, null, "author");
        }

        #endregion
    }
}
