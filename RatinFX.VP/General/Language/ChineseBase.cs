namespace RatinFX.VP.General.Language
{
    /// <summary>
    /// Must override <see cref="LanguageBase.GetTranslation(System.Enum)"/>
    /// with the project related Translation Keys
    /// </summary>
    public class ChineseBase : LanguageBase
    {
        public static string Short => "zh";
        public override string ShortName => Short;
        public override string DisplayName => "中文";
    }
}
