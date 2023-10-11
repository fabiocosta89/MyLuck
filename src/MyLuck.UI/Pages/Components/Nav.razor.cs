namespace MyLuck.UI.Pages.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Security.Claims;

public partial class Nav
{
    [Inject]
    private AuthenticationStateProvider? _authenticationStateProvider { get; set; }

    private Dictionary<string, string> _items = new()
    {
        { "Lotto", "/lotto" },
        { "Euromillions", "" },
        { "High5", "" }
    };

    protected ClaimsPrincipal? ClaimsUser { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (_authenticationStateProvider != null)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            ClaimsUser = authState?.User;
        }
    }
}
