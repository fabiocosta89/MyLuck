namespace MyLuck.UI.Pages.Components;

using Microsoft.AspNetCore.Components;

using MyLuck.Infrastructure.Features.Key;

public partial class MyNumbers
{
    [Parameter]
    public IEnumerable<Key> Keys { get; set; } = Enumerable.Empty<Key>();
}
