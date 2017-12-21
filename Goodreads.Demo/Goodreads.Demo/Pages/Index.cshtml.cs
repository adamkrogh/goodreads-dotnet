using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Goodreads.Demo.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            // Create an unauthorized Goodreads client.
            var client = GoodreadsClient.Create(Storage.ApiKey, Storage.ApiSecret);

            // Ask a Goodreads request token and build an authorization url.
            var callbackUrl = Url.Page("Callback", null, null, Request.Scheme);
            var requestToken = await client.AskCredentials(callbackUrl);

            // Save user token for future used.
            Storage.SaveToken(requestToken.Token, requestToken.Secret);

            // Redirect to Goodreads auth page.
            return Redirect(requestToken.AuthorizeUrl);
        }
    }
}
