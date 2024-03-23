using System.Collections.Generic;

namespace RatinFX.VP.General.Language
{
    public class LanguageBase
    {
        public virtual string ShortName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual Dictionary<string, string> Translation { get; set; } = new Dictionary<string, string>();
        public virtual Dictionary<string, string> Create() => new Dictionary<string, string>();
    }
}
