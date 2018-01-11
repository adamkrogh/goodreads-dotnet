using Goodreads.Http;
using System.Threading.Tasks;

namespace Goodreads
{
    /// <summary>
    /// Define behavior for OAuth authorization and getting the Goodreads access token.
    /// </summary>
    public interface IAuthManager : IApiCredentialsInfoManager
    {
        /// <summary>
        /// Ask the Goodreads credentials.
        /// </summary>
        /// <param name="callbackUrl">A desire callback url. User will be redirected here after login into Goodreads.</param>
        /// <returns>OAuth request token with authorize url.</returns>
        Task<OAuthRequestToken> AskCredentials(string callbackUrl);

        /// <summary>
        /// Get the Goodreads access token using OAuth request token.
        /// </summary>
        /// <param name="token">A specified request token.</param>
        /// <returns>OAuth access token.</returns>
        Task<OAuthAccessToken> GetAccessToken(OAuthRequestToken token);

        /// <summary>
        /// Get the Goodreads access token using OAuth request token and OAuth secret.
        /// </summary>
        /// <param name="token">A specified request token.</param>
        /// <param name="secret">A specified secret.</param>
        /// <returns>OAuth access token.</returns>
        Task<OAuthAccessToken> GetAccessToken(string token, string secret);
    }
}
