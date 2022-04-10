using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Zenly.Analytics.Console;
using Zenly.Analytics.Console.Settings;

namespace Zenly.Analytics.ConsoleApp
{
    internal class AppSettings
    {
        [JsonIgnore]
        internal static readonly string FileExceptionMessage = "An error occurred while reading the configuration file";

#if DEBUG
        [JsonIgnore]
        internal static readonly string FileName = "appsettings.Development.json";
#else
        [JsonIgnore]
        internal static readonly string FileName = "appsettings.json";
#endif

        [JsonProperty("connectionString")]
        internal string ConnectionString { set; get; }

        [JsonProperty("databaseCommandSettings")]
        internal DataBaseCommandSettings DataBaseCommand { set; get; }

        [JsonProperty("notificationCommandSettings")]
        internal NotificationCommandSettings Notification { set; get; }

        [JsonProperty("statusCommandSettings")]
        internal StatusCommandSettings Status { set; get; }

        [JsonProperty("languageDataSet")]
        internal LanguageDataSet LanguageDataSet { set; get; }

        internal static AppSettings LoadFromFile()
        {
            AppSettings appSettings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), FileName)));
            return appSettings;
        }
        internal void SaveToFile()
        {
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), FileName), JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
    internal class LanguageDataSet
    {
        internal string GetValue(string memberName)
        {
            Dictionary<string, string> keyValuePairs = (Dictionary<string, string>)GetType().GetProperty(memberName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this);
            if (keyValuePairs.TryGetValue(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, out string value))
            {
                return value;
            }

            value = keyValuePairs.ToList().FirstOrDefault().Value;
            value = string.IsNullOrEmpty(value) ? string.Empty : value;
            return value;
        }

        [JsonProperty("General.Unexpected")]
        internal Dictionary<string, string> GeneralUnexpected { set; get; }

        [JsonProperty("Discord.Connected")]
        internal Dictionary<string, string> DiscordConnected { set; get; }

        [JsonProperty("Discord.Disconnected")]
        internal Dictionary<string, string> DiscordDisconnected { set; get; }

        [JsonProperty("General.Complete")]
        internal Dictionary<string, string> GeneralComplete { set; get; }

        [JsonProperty("Daemon.UserNotExits")]
        internal Dictionary<string, string> DaemonUserNotExits { set; get; }

        [JsonProperty("ZenlyApi.Ok")]
        internal Dictionary<string, string> ZenlyAPIOk { set; get; }

        [JsonProperty("ZenlyApi.Error")]
        internal Dictionary<string, string> ZenlyError { set; get; }
    }
}
