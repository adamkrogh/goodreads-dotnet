using Goodreads.Clients;
using Goodreads.Http;
using RestSharp;

namespace Goodreads
{
    public class GoodreadsClient : IGoodreadsClient
    {
        public static readonly string GoodreadsUrl = " https://www.goodreads.com/";
        private readonly IConnection _connection;

        public GoodreadsClient(string token)
        {
            var client = new RestClient(GoodreadsUrl)
            {
                UserAgent = "goodreads-dotnet"
            };
            //client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            client.AddDefaultParameter("key", token, ParameterType.QueryString);
            client.AddDefaultParameter("format", "xml", ParameterType.QueryString);

            _connection = new Connection(client);

            Authors = new AuthorsClient(_connection);
        }

        #region IGoodreadsClient Members

        public IAuthorsClient Authors { get; private set; }

        #endregion
    }
}