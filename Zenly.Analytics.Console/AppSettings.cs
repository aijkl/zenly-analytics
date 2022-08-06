using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public AppSettings(string connectionString, DataBaseCommandSettings dataBaseCommand, NotificationCommandSettings notification, StatusCommandSettings status, LanguageDataSet languageDataSet)
        {
            ConnectionString = connectionString;
            DataBaseCommand = dataBaseCommand;
            Notification = notification;
            Status = status;
            LanguageDataSet = languageDataSet;
        }

        [JsonProperty("connectionString")]
        [JsonRequired]
        internal string ConnectionString { set; get; }

        [JsonProperty("databaseCommandSettings")]
        [JsonRequired]
        internal DataBaseCommandSettings DataBaseCommand { set; get; }

        [JsonProperty("notificationCommandSettings")]
        internal NotificationCommandSettings Notification { set; get; }

        [JsonProperty("statusCommandSettings")]
        internal StatusCommandSettings Status { set; get; }

        [JsonProperty("languageDataSet")]
        internal LanguageDataSet LanguageDataSet { set; get; }

        internal static AppSettings LoadFromFile()
        {
            return JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), FileName))) ?? throw new ApplicationException(FileExceptionMessage);
        }
        internal void SaveToFile()
        {
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), FileName), JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
    internal class LanguageDataSet
    {
        public LanguageDataSet(Dictionary<string, string> generalUnexpected, Dictionary<string, string> discordConnected, Dictionary<string, string> discordDisconnected, Dictionary<string, string> generalComplete, Dictionary<string, string> daemonUserNotExits, Dictionary<string, string> zenlyApiOk, Dictionary<string, string> zenlyError)
        {
            GeneralUnexpected = generalUnexpected;
            DiscordConnected = discordConnected;
            DiscordDisconnected = discordDisconnected;
            GeneralComplete = generalComplete;
            DaemonUserNotExits = daemonUserNotExits;
            ZenlyAPIOk = zenlyApiOk;
            ZenlyError = zenlyError;
        }

        internal string GetValue(string memberName)
        {
            var keyValuePairs = (Dictionary<string, string>?)GetType().GetProperty(memberName, BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this);
            if (keyValuePairs == null) throw new ArgumentNullException(nameof(keyValuePairs), "Invalid memberName");

            if (keyValuePairs.TryGetValue(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, out var value))
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
