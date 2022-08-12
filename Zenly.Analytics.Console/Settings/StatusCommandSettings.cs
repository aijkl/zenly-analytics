using System.Collections.Generic;
using Newtonsoft.Json;
using Zenly.Analytics.Console.DiscordRpc;

namespace Zenly.Analytics.Console.Settings
{
    internal class StatusCommandSettings
    {
        public StatusCommandSettings(string token, string zenlyToken, int pollingIntervalMs, string userId, string scribanMeterFromHome, string scribanLocationName, StatusLocation home, List<StatusLocation> locations)
        {
            Token = token;
            ZenlyToken = zenlyToken;
            PollingIntervalMs = pollingIntervalMs;
            UserId = userId;
            ScribanMeterFromHome = scribanMeterFromHome;
            ScribanLocationName = scribanLocationName;
            Home = home;
            Locations = locations;
        }

        [JsonProperty("token", Required = Required.Always)]
        public string Token { set; get; }

        [JsonProperty("zenlyToken", Required = Required.Always)]
        public string ZenlyToken { set; get; }

        [JsonProperty("pollingIntervalMs", Required = Required.Always)]
        public int PollingIntervalMs { set; get; }

        [JsonProperty("userId", Required = Required.Always)]
        public string UserId { set; get; }

        [JsonProperty("scribanMeterFromHome", Required = Required.Always)]
        public string ScribanMeterFromHome { set; get; }

        [JsonProperty("scribanLocationName", Required = Required.Always)]
        public string ScribanLocationName { set; get; }

        [JsonProperty("home", Required = Required.Always)]
        public StatusLocation Home { set; get; }

        [JsonProperty("locations", Required = Required.Always)]
        public List<StatusLocation> Locations { set; get; }
    }
}
