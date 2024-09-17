using RatinFX.VP.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RatinFX.VP.General.Language
{
    public class LanguageConfig
    {
        private readonly string _filePath;
        public string LastUpdatedVersion { get; set; } = "0.0.0";
        public string Current { get; set; } = EnglishBase.Short;
        public List<LanguageBase> Translations { get; set; } = new List<LanguageBase>();

        public LanguageConfig() { }
        public LanguageConfig(bool init, string filePath, string currentVersion, List<LanguageBase> languages)
        {
            try
            {
                if (!init)
                    return;

                _filePath = filePath;

                var config = FileHandler.LoadVPConfig(this, _filePath);
                if (config != null && config.Translations.Count > 0 && config.Translations.Count >= languages.Count)
                {
                    LastUpdatedVersion = config.LastUpdatedVersion;
                    Current = config.Current;
                    Translations = config.Translations;
                }
                else
                {
                    ResetLanguages(currentVersion, languages);
                }

                // Should force update translations just in case
                if (LastUpdatedVersion != currentVersion || Translations.Any(x => x.Translation.Count < 1))
                {
                    LastUpdatedVersion = currentVersion;

                    foreach (var l in languages)
                    {
                        var tr = Translations.Find(x =>
                            x.ShortName == l.ShortName ||
                            x.DisplayName == l.DisplayName
                        );

                        if (tr == null)
                            continue;

                        tr.ShortName = l.ShortName;
                        tr.DisplayName = l.DisplayName;
                        tr.Translation = l.Create();
                    }
                }

                Save();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                ResetLanguages(currentVersion, languages);
                Save();
            }
        }

        public void Save()
        {
            FileHandler.SaveVPConfig(this, _filePath);
        }

        public void ResetLanguages(string currentVersion, List<LanguageBase> languages)
        {
            LastUpdatedVersion = currentVersion;
            Current = EnglishBase.Short;
            Translations = languages;
        }

        public string CurrentLanguageDisplay()
        {
            var curr = Translations.Find(x => x.ShortName == Current)
                ?? Translations.Find(x => x.ShortName == EnglishBase.Short)
                ?? Translations.FirstOrDefault();

            return curr?.DisplayName ?? "ERROR";
        }

        public void SetLanguage(string language)
        {
            var curr = Translations.Find(x => x.DisplayName == language)
                ?? Translations.Find(x => x.ShortName == EnglishBase.Short)
                ?? Translations.FirstOrDefault();

            Current = curr?.ShortName ?? EnglishBase.Short;
            Save();
        }

        public string Find(string key)
        {
            var lang = Translations.Find(x => x.ShortName == Current);
            if (lang != null && lang.Translation.ContainsKey(key))
                return lang.Translation[key];

            lang = Translations.Find(x => x.ShortName == EnglishBase.Short);
            if (lang != null && lang.Translation.ContainsKey(key))
                return lang.Translation[key];

            lang = Translations.FirstOrDefault();
            if (lang != null && lang.Translation.ContainsKey(key))
                return lang.Translation[key];

            return $"ERROR: {key}";
        }
    }
}
