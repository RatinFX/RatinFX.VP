using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace RatinFX.VP.Helpers
{
    public class Helper
    {
        private static string GetContentFromURL(string project)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;

            using var newClient = new HttpClient();
            return newClient.GetStringAsync($"https://ratinfx.github.io/version/{project}").Result;
        }

        private static void CheckForUpdate(
            string project,
            string current,
            Action<string> latest = null,
            Action<string> info = null
            )
        {
            try
            {
                var content = GetContentFromURL(project);

                var latestVersion = Regex.Match(content, Parameters.VersionPattern).Value;

                if (string.IsNullOrEmpty(latestVersion) || latestVersion.Length < 6)
                {
                    info?.Invoke("Failed to check for Update.");
                    return;
                }

                var updateAvailable = !current.Equals(latestVersion);
                latest?.Invoke(updateAvailable ? latestVersion : null);

            }
            catch (Exception ex)
            {
                info?.Invoke("Failed to check for Update.\n" + ex.Message);
                return;
            }
        }

        public static void CheckForUpdate_VegasProFlow(string current, Action<string> latest = null, Action<string> info = null)
        {
            CheckForUpdate("vegas-pro-flow", current, latest, info);
        }

        public static void CheckForUpdate_BetterSearch(string current, Action<string> latest = null, Action<string> info = null)
        {
            CheckForUpdate("better-search", current, latest, info);
        }
    }
}
