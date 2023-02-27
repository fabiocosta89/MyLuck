namespace MyLuck.Service.Features.Lotto;
using MongoDB.Bson;

using MyLuck.Infrastructure.Features.Lotto;
using MyLuck.Service.Helpers;
using MyLuck.Service.Models;

using System;
using System.Linq;

internal static class LottoMappings
{
    internal static Lotto? LoterieDrawsToLotto(LoterieDraws<LoterieDraw<LottoResult>> lotto)
    {
        var lottoDraw = lotto.Draws?.FirstOrDefault();
        if (lottoDraw == null
            || lottoDraw.Results == null)
        {
            return null;
        }

        DateTime drawTime = DateTimeHelper.ConvertLinuxDateTimeIntoDateTime(lottoDraw.DrawTime);

        return new Lotto
        {
            Id = ObjectId.GenerateNewId().ToString(),
            DrawId = lottoDraw.DrawId,
            DrawTime = drawTime,
            DrawTimeValue = lottoDraw.DrawTime,
            Number1 = lottoDraw.Results?[0].Primary?[0],
            Number2 = lottoDraw.Results?[0].Primary?[1],
            Number3 = lottoDraw.Results?[0].Primary?[2],
            Number4 = lottoDraw.Results?[0].Primary?[3],
            Number5 = lottoDraw.Results?[0].Primary?[4],
            Number6 = lottoDraw.Results?[0].Primary?[5],
            Special = lottoDraw.Results?[0].Secondary?[0]
        };
    }
}
