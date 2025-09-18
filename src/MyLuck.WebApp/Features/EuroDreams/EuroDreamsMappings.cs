using System.Collections.ObjectModel;
using MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;

namespace MyLuck.WebApp.Features.EuroDreams;
using System.Linq;
using MyLuck.Infrastructure.Features.EuroDreams;
internal static class EuroDreamsMappings
{
    internal static Collection<EuroDreams> LoterieResultToEuroDreams(IEnumerable<LotteryResult> lotteryResults)
    {
        Collection<EuroDreams> euroDreams = [];
        
        foreach (var result in lotteryResults)
        {
            euroDreams.Add(new EuroDreams(
                result.Numbers.Where(n => !n.IsSpecial).Select(x => x.Value).Order().ToArray(), 
                result.Numbers.Where(n => n.IsSpecial).Select(x => x.Value).ToArray(), 
                result.Date));
        }

        return euroDreams;
    }
}
