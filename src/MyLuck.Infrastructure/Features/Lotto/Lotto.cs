namespace MyLuck.Infrastructure.Features.Lotto;

using MyLuck.Infrastructure.Models;

public class Lotto : Draw
{
    public int? Number1 { get; set; }
    public int? Number2 { get; set; }
    public int? Number3 { get; set; }
    public int? Number4 { get; set; }
    public int? Number5 { get; set; }
    public int? Number6 { get; set; }
    public int? Special { get; set; }
}
