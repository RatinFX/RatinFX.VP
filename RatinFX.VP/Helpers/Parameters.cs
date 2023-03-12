using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatinFX.VP.Helpers
{
    internal static class Parameters
    {
        public static string VersionPattern => @"(?<=\[version=)(.*?)(?=\])";
    }
}
