using Auth0.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyLuck.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        public async Task OnGetAsync()
        {
            string returnUrl = Request.Query["ReturnUrl"][0] ?? "/";
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            // It's needed because production runs at http locally
            HttpContext.Request.Scheme = "https";

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }
    }
}
