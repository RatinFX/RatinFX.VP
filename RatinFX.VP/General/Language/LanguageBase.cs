using System;
using System.Collections.Generic;

namespace RatinFX.VP.General.Language
{
    public class LanguageBase
    {
        /// <summary>
        /// Shorter name used in the Settings file,
        /// usually overridden by the value of a
        /// `static` field in an extension of this class
        /// </summary>
        public virtual string ShortName { get; set; }

        /// <summary>
        /// Displayed name in the App settings
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Must be overridden with the project related Translation Keys 
        /// </summary>
        /// <param name="key">The Translation Key to search for</param>
        /// <returns>The Translated string</returns>
        public virtual string GetTranslation(Enum key) => string.Empty;

        /// <summary>
        /// The list containing every Translation for this language
        /// </summary>
        public Dictionary<string, string> Translation { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Runs after updating to a newer version of the app
        /// (or if the LanguageConfig file was not found)
        /// to always have up-to-date translations
        /// </summary>
        /// <typeparam name="T">Enum that contains all the Translation Keys</typeparam>
        /// <returns>All Translation Key-Value pairs for the given language</returns>
        public Dictionary<string, string> Create<T>()
            where T : Enum
        {
            var translations = new Dictionary<string, string>();

            foreach (T key in Enum.GetValues(typeof(T)))
            {
                var name = key.ToString();
                var value = GetTranslation(key);

                if (value != null)
                {
                    translations[name] = value;
                }
            }

            return translations;
        }
    }
}
