namespace MyLuck.UI.Pages.Components;

using Microsoft.AspNetCore.Components;

using MyLuck.Infrastructure.Features.Key;

public partial class ResultsTable
{
    [Parameter]
    public required string[] Header { private get; set; } = Array.Empty<string>();

    [Parameter]
    public required IList<int[]> NormalValues { private get; set; } = new List<int[]>();

    [Parameter]
    public required IList<int[]> SpecialValues { private get; set; } = new List<int[]>();

    [Parameter]
    public required DateTime[] Dates { private get; set; }

    [Parameter]
    public IEnumerable<Key> Keys { get; set; } = Enumerable.Empty<Key>();
}
