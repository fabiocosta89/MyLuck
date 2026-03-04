using System.Collections.ObjectModel;
using MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;

namespace MyLuck.WebApp.Features.EuroDreams;

internal static class EuroDreamsMappings
{
    internal static Collection<Infrastructure.Features.EuroDreams.EuroDreams> LoterieResultToEuroDreams(IEnumerable<LotteryResult> lotteryResults)
    {
        Collection<Infrastructure.Features.EuroDreams.EuroDreams> euroDreams = [];
        
        foreach (var result in lotteryResults)
        {
            euroDreams.Add(new Infrastructure.Features.EuroDreams.EuroDreams(
                result.Numbers.Where(n => !n.IsSpecial).Select(x => x.Value).Order().ToArray(), 
                result.Numbers.Where(n => n.IsSpecial).Select(x => x.Value).ToArray(), 
                result.Date));
        }

        return euroDreams;
    }
}
