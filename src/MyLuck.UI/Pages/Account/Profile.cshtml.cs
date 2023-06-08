using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Claims;

namespace MyLuck.UI.Pages
{
    [Authorize(Roles = "User")]
    public class ProfileModel : PageModel
    {
        public void OnGet()
        {
            var Name = User.Identity?.Name;
            var    EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;
            
        }
    }
}
