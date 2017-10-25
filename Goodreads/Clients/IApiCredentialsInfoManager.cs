using Goodreads.Http;

namespace Goodreads
{
    public interface IApiCredentialsInfoManager
    {
        ApiCredentials GetCredentials();
    }
}
