namespace RatinFX.VP.General.Language
{
    /// <summary>
    /// Must override <see cref="LanguageBase.GetTranslation(System.Enum)"/>
    /// with the project related Translation Keys
    /// </summary>
    public class EnglishBase : LanguageBase
    {
        public static string Short => "en";
        public override string ShortName => Short;
        public override string DisplayName => "English";
    }
}
