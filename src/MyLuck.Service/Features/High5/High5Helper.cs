namespace MyLuck.Service.Features.High5;

using MyLuck.Infrastructure.Features.High5;

internal static class High5Helper
{
    internal static int?[] ModelIntoArray(High5 high5)
    {
        int?[] drawKeys =
        {
            high5.Number1,
            high5.Number2,
            high5.Number3,
            high5.Number4,
            high5.Number5
        };

        return drawKeys;
    }
}
