using Goodreads.Http;

namespace Goodreads.Clients
{
    internal abstract class Endpoint
    {
        protected IConnection Connection { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Endpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        protected Endpoint(IConnection connection)
        {
            Connection = connection;
        }
    }
}
