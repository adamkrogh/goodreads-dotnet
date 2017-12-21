using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Goodreads.Demo.Pages
{
    public class CallbackModel : PageModel
    {
        public string Message { get; private set; }

        /// <summary>
        /// Callback get handler.
        /// </summary>
        /// <param name="oauth_token">A public OAuth token.</param>
        /// <param name="authorize">Determine whether user has been already auth or not.</param>
        /// <returns></returns>
        public async Task OnGetAsync(string oauth_token, int authorize)
        {
            if (authorize == 0)
            {
                Message = $"Oops, seems you didn't grant an access for the Demo application.";
                return;
            }

            // Create an unauthorized Goodreads client.
            var client = GoodreadsClient.Create(Storage.ApiKey, Storage.ApiSecret);

            // Get a user's OAuth access token and secret after they have granted access.
            var accessToken = await client.GetAccessToken(oauth_token, Storage.GetSecretToken(oauth_token));

            // Create an authorized Goodreads client.
            var authClient = GoodreadsClient.CreateAuth(Storage.ApiKey, Storage.ApiSecret, accessToken.Token, accessToken.Secret);

            // Get information for the current user.
            var currentUserId = await authClient.Users.GetAuthenticatedUserId();
            var user = await authClient.Users.GetByUserId(currentUserId);

            Message = $"Welcome {user.Name}";
        }
    }
}
