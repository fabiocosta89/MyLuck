namespace MyLuck.UI.Pages.Components;

using Microsoft.AspNetCore.Components;

using MyLuck.Infrastructure.Features.Key;
using MyLuck.UI.Features.Keys;

public partial class MyNumbers
{
    [Inject]
    private IKeyDataService? KeyDataService { get; set; }

    private IEnumerable<Key> _keys = Enumerable.Empty<Key>();

    protected override async Task OnInitializedAsync()
    {
        if (KeyDataService is not null)
        {
            _keys = await KeyDataService.GetAsync(LottoConstants.Name);
        }
    }
}
