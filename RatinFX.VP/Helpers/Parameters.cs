using System;
using System.Reflection;

namespace RatinFX.VP.Helpers
{
    internal static class Parameters
    {
        public static string VersionPattern => @"(?<=\[version=)(.*?)(?=\])";
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public static string CurrentVersion => $"{Version.Major}.{Version.Minor}.{Version.Build}.";
    }
}
