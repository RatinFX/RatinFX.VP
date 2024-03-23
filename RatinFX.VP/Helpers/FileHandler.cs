using Newtonsoft.Json;
using System;
using System.IO;

namespace RatinFX.VP.Helpers
{
    /// <summary>
    /// General and VEGAS Pro Config JSON data handler
    /// </summary>
    public class FileHandler
    {
        public static T CreateReadConfig<T>(T config, string filePath)
            where T : class
        {
            if (!File.Exists(filePath))
                SaveConfig(config, filePath);

            var file = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(file);
        }

        public static void SaveConfig<T>(T config, string filePath)
            where T : class
        {
            var serialized = JsonConvert.SerializeObject(config, Formatting.Indented);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, serialized);
        }

        public static string DEFAULT_PATH =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            $@"\Vegas Application Extensions\";

        /// <summary>
        /// Creates a VEGAS Pro related folder path under <see cref="DEFAULT_PATH"/>
        /// </summary>
        public static string GetVPFolder(string folderName)
        {
            var path = DEFAULT_PATH + folderName;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            return path;
        }

        /// <summary>
        /// Creates a VEGAS Pro related Config file path under <see cref="DEFAULT_PATH"/>
        /// </summary>
        public static string GetVPConfigPath(string fileName)
        {
            return DEFAULT_PATH + fileName + ".json";
        }

        /// <summary>
        /// Loads a VEGAS Pro related Config file
        /// </summary>
        public static T LoadVPConfig<T>(T config, string fileName)
            where T : class
        {
            return CreateReadConfig(config, GetVPConfigPath(fileName));
        }

        /// <summary>
        /// Saves a VEGAS Pro related Config file
        /// </summary>
        public static void SaveVPConfig<T>(T config, string fileName)
            where T : class
        {
            SaveConfig(config, GetVPConfigPath(fileName));
        }
    }
}
