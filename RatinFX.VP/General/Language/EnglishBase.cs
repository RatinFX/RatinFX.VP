using System.Collections.Generic;

namespace RatinFX.VP.General.Language
{
    /// <summary>
    /// Must override <see cref="LanguageBase.Create"/> to create default translation text
    /// </summary>
    public class EnglishBase : LanguageBase
    {
        public static string Short => "en";
        public override string ShortName => Short;
        public override string DisplayName => "English";
        public override Dictionary<string, string> Translation => Create();
    }
}
