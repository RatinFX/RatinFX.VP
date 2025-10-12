using System;

namespace RatinFX.VP.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToDebugString(this DateTime time)
        {
            return time.ToString("yyyy-mm-dd_HH-mm-ss");
        }
    }
}
