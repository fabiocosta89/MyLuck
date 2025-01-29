namespace MyLuck.WebApp.Features.EuroDreams;
using MongoDB.Bson;

using MyLuck.Infrastructure.Features.EuroDreams;
using MyLuck.WebApp.Features.Shared.Helpers;
using MyLuck.WebApp.Features.Shared.Lottery;

using System;
using System.Linq;

internal static class EuroDreamsMappings
{
    internal static EuroDream? LoterieDrawsToEuroDream(LoterieDraws<LoterieDraw<EuroDreamsResult>> euroDream)
    {
        var euroDreamDraw = euroDream.Draws?.FirstOrDefault();
        if (euroDreamDraw == null
            || euroDreamDraw.Results == null)
        {
            return null;
        }

        DateTime drawTime = DateTimeHelper.ConvertLinuxDateTimeIntoDateTime(euroDreamDraw.DrawTime);

        return new EuroDream
        {
            Id = ObjectId.GenerateNewId().ToString(),
            DrawId = euroDreamDraw.DrawId,
            DrawTime = drawTime,
            DrawTimeValue = euroDreamDraw.DrawTime,
            Number1 = euroDreamDraw.Results?[0].Primary?[0],
            Number2 = euroDreamDraw.Results?[0].Primary?[1],
            Number3 = euroDreamDraw.Results?[0].Primary?[2],
            Number4 = euroDreamDraw.Results?[0].Primary?[3],
            Number5 = euroDreamDraw.Results?[0].Primary?[4],
            Number6 = euroDreamDraw.Results?[0].Primary?[5],
            Special = euroDreamDraw.Results?[0].Secondary?[0]
        };
    }
}
