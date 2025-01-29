namespace MyLuck.WebApp.Features.Shared.Helpers;
using System;

internal static class DateTimeHelper
{
    /// <summary>
    /// Convert from a Linux datetime decimal value into a C# datetime
    /// </summary>
    /// <param name="linuxTime">decimal value</param>
    /// <returns>Datetime instance</returns>
    internal static DateTime ConvertLinuxDateTimeIntoDateTime(decimal? linuxTime)
    {
        if (linuxTime is null) { return default; }

        DateTime drawTime = DateTime.UnixEpoch;
        double drawTimeDouble = Convert.ToDouble(linuxTime);
        drawTime = drawTime
            .AddMilliseconds(drawTimeDouble)
            .ToUniversalTime();

        return drawTime;
    }
}
