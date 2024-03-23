using System.Collections.Generic;

namespace RatinFX.VP.General.Language
{
    /// <summary>
    /// Must override <see cref="LanguageBase.Create"/> to create default translation text
    /// </summary>
    public class ChineseBase : LanguageBase
    {
        public static string Short => "zh";
        public override string ShortName => Short;
        public override string DisplayName => "中文";
        public override Dictionary<string, string> Translation => Create();
    }
}
