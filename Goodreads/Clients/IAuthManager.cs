using Goodreads.Http;
using System.Threading.Tasks;

namespace Goodreads
{
    public interface IAuthManager : IApiCredentialsInfoManager
    {
        Task<OAuthRequestToken> AskCredentials(string callbackUrl);

        Task<OAuthAccessToken> GetAccessToken(OAuthRequestToken token);
    }
}
