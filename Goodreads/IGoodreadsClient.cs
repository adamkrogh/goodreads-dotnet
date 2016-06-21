using Goodreads.Clients;

namespace Goodreads
{
    public interface IGoodreadsClient
    {
        IAuthorsClient Authors { get; }
    }
}
