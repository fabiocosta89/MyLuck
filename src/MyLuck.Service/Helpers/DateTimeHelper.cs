namespace MyLuck.Service.Helpers;
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
        if (linuxTime is null) { return default(DateTime); }

        DateTime drawTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        double drawTimeDouble = Convert.ToDouble(linuxTime);
        drawTime = drawTime
            .AddMilliseconds(drawTimeDouble)
            .ToUniversalTime();

        return drawTime;
    }
}
