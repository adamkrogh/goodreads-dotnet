using Goodreads.Http;

namespace Goodreads.Clients
{
    internal abstract class EndpointClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        protected EndpointClient(IConnection connection)
        {
            Connection = connection;
        }

        protected IConnection Connection { get; }
    }
}
