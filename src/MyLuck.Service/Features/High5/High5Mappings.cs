namespace MyLuck.Service.Features.High5;
using MongoDB.Bson;

using MyLuck.Infrastructure.Features.High5;
using MyLuck.Service.Helpers;
using MyLuck.Service.Models;

internal static class High5Mappings
{
    internal static High5? LoterieDrawsToHigh5(LoterieDraws<LoterieDraw<LoterieResult>> high5Model)
    {
        var high5Draw = high5Model.Draws?.FirstOrDefault();
        if (high5Draw == null
            || high5Draw.Results == null)
        {
            return null;
        }

        DateTime drawTime = DateTimeHelper.ConvertLinuxDateTimeIntoDateTime(high5Draw.DrawTime);

        return new High5
        {
            Id = ObjectId.GenerateNewId().ToString(),
            DrawId = high5Draw.DrawId,
            DrawTime = drawTime,
            DrawTimeValue = high5Draw.DrawTime,
            Number1 = high5Draw.Results?[0].Primary?[0],
            Number2 = high5Draw.Results?[0].Primary?[1],
            Number3 = high5Draw.Results?[0].Primary?[2],
            Number4 = high5Draw.Results?[0].Primary?[3],
            Number5 = high5Draw.Results?[0].Primary?[4],
        };
    }
}
