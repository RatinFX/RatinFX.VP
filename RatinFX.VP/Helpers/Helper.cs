using Octokit;
using System;
using System.Net;

namespace RatinFX.VP.Helpers
{
    public class Helper
    {
        private static readonly GitHubClient _client = new GitHubClient(new ProductHeaderValue("RatinFX.VP_" + GetCurrentUnixTime()));
        internal static string GitHubUsername = "RatinFX";

        public static void CheckForUpdate(
            string project,
            string current,
            Action<string> latest = null,
            Action<string> info = null
            )
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;

                var latestRelease = _client.Repository.Release.GetLatest(GitHubUsername, project)?.Result;

                if (string.IsNullOrEmpty(latestRelease.TagName))
                {
                    info?.Invoke("Failed to check for Update.");
                    return;
                }

                var updateAvailable = current != latestRelease.TagName;
                latest?.Invoke(updateAvailable ? latestRelease.TagName : null);
            }
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                var msg = "Failed to check for Update.";

                if (ex is RateLimitExceededException)
                {
                    // Let's not show the IP of the user for now...
                    var rateLimited = ex as RateLimitExceededException;
                    var remaining = rateLimited.Reset.Subtract(DateTimeOffset.Now).TotalMinutes;
                    msg += $"GitHub API rate limit exceeded, remaining minutes until reset: {remaining:0.00}";
                }
                else
                {
                    msg += $"\n\n- {ex.Message}";
                }

                latest?.Invoke(null);
                info?.Invoke(msg);
                return;
            }
        }
        
        public static bool ShouldCheckForUpdate(long lastChecked)
        {
            return GetCurrentUnixTime() - lastChecked >= 3_600
                || lastChecked < 0;
        }

        public static long GetCurrentUnixTime()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
