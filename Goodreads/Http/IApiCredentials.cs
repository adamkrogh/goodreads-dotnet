using Goodreads.Models;

namespace Goodreads.Http
{
    /// <summary>
    /// Represents credentials used to access the Goodreads API.
    /// </summary>
    public interface IApiCredentials
    {
        /// <summary>
        /// Gets a request token from Goodreads and builds an authorize
        /// URL to redirect the user to.
        /// </summary>
        /// <param name="callbackUrl">The callback URL that Goodreads will redirect back to.</param>
        /// <returns>An authorize URL that the user can be redirected to.</returns>
        string GetRequestTokenAndBuildAuthorizeUrl(string callbackUrl = null);

        /// <summary>
        /// Build an OAuth authorization URL. This method uses
        /// the token passed into the GoodreadsClient constructor.
        /// Use the overload to change this.
        /// </summary>
        /// <param name="callbackUrl">The URL Goodreads will redirect back to.</param>
        /// <returns>A URL to authorize an OAuth request token.</returns>
        string BuildAuthorizeUrl(string callbackUrl = null);

        /// <summary>
        /// Build an OAuth authorization URL with the given OAuth token and callback URL.
        /// </summary>
        /// <param name="oauthToken">The OAuth request token to authorize.</param>
        /// <param name="callbackUrl">The URL Goodreads will redirect back to.</param>
        /// <returns>A URL to authorize an OAuth request token.</returns>
        string BuildAuthorizeUrl(string oauthToken, string callbackUrl);

        /// <summary>
        /// Get a request token from Goodreads using the API key and secret
        /// used in the client instantiation.
        /// </summary>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        ApiCredentials GetRequestToken();

        /// <summary>
        /// Get a request token from the Goodreads API using the given API
        /// key and secret.
        /// </summary>
        /// <param name="apiKey">A Goodreads API key.</param>
        /// <param name="apiSecret">A Goodreads API secret.</param>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        ApiCredentials GetRequestToken(string apiKey, string apiSecret);

        /// <summary>
        /// Get an access token from the Goodreads API.
        /// </summary>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        ApiCredentials GetAccessToken();

        /// <summary>
        /// Get an access token from the Goodreads API using the given credentials.
        /// </summary>
        /// <param name="apiKey">A Goodreads API key.</param>
        /// <param name="apiSecret">A Goodreads API secret.</param>
        /// <param name="oauthRequestToken">An OAuth request token that has been authorized.</param>
        /// <param name="oauthRequestTokenSecret">An OAuth request token secret that has been authorized.</param>
        /// <returns>A set of API credentials with request tokens populated.</returns>
        ApiCredentials GetAccessToken(string apiKey, string apiSecret, string oauthRequestToken, string oauthRequestTokenSecret);

        /// <summary>
        /// Get the authenticated user's Goodreads user Id.
        /// Will only work if you have initialized the client with
        /// OAuth tokens or retrieved an access token with the client.
        /// </summary>
        /// <returns>The logged in user's Goodreads user Id.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request.")]
        int GetUserId();
    }
}
