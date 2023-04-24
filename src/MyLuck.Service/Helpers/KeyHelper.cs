namespace MyLuck.Service.Helpers;
using MyLuck.Infrastructure.Features.Key;

using System.Text;

internal static class KeyHelper
{
    internal static StringBuilder CheckMatching(
        IEnumerable<Key> keys,
        int?[] drawKeys,
        int?[] drawSpecialKeys)
    {
        StringBuilder keyStringBuilder = new();

        foreach (Key key in keys)
        {
            if (key.Numbers == null) continue;

            keyStringBuilder.AppendLine("<br><br>My numbers: ");
            key.Numbers.ToList().ForEach(num => keyStringBuilder.Append($" {num}"));
            if (key.SpecialNumbers is not null)
            {
                keyStringBuilder.Append(" - ");
                key.SpecialNumbers.ToList().ForEach(num => keyStringBuilder.Append($" {num}"));
            }

            IEnumerable<int> matchNumbers = key.Numbers.Where(num => drawKeys.Contains(num));
            if (matchNumbers.Any())
            {
                keyStringBuilder.AppendLine("<br><br>Matching numbers: ");
                matchNumbers.ToList().ForEach(num => keyStringBuilder.Append($" {num}"));
            }

            if (drawSpecialKeys.Any() && key.SpecialNumbers is not null)
            {
                IEnumerable<int> matchSpecial = key.SpecialNumbers.Where(num => drawSpecialKeys.Contains(num));
                if (matchSpecial.Any())
                {
                    keyStringBuilder.AppendLine("<br><br>Matching special numbers: ");
                    matchSpecial.ToList().ForEach(num => keyStringBuilder.Append($" {num}"));
                }
            }
        }

        return keyStringBuilder;
    }
}
