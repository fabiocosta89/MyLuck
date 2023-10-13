namespace MyLuck.UI.Pages.Components;

using Microsoft.AspNetCore.Components;

using MyLuck.Infrastructure.Features.Key;

public partial class BoldNumber
{
    private bool _found;

    [Parameter]
    public int? Number { get; set; }

    [Parameter]
    public bool Special { get; set; }

    [Parameter]
    public IEnumerable<Key> Keys { get; set; } = Enumerable.Empty<Key>();


    protected override void OnInitialized()
    {
        if (Special)
        {
            _found = Keys.Any(k => k.SpecialNumbers is not null
                && Array.Exists(k.SpecialNumbers, n => n == Number));

            return;
        }

        _found = Keys.Any(k => k.Numbers is not null
                && Array.Exists(k.Numbers, n => n == Number));
    }
}
