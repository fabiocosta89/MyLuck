using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyLuck.UI.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public string? Email { get; set; }
        public string? NickName { get; set; }

        public void OnGet()
        {
            Email = User.Identity?.Name;
            NickName = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value;
        }
    }
}
