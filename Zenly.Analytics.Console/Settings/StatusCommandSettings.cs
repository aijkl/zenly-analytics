using System.Collections.Generic;
using Newtonsoft.Json;
using Zenly.Analytics.Console.DiscordRpc;

namespace Zenly.Analytics.Console.Settings
{
    internal class StatusCommandSettings
    {
        [JsonProperty("token")]
        public string Token { set; get; }

        [JsonProperty("zenlyToken")]
        public string ZenlyToken { set; get; }

        [JsonProperty("pollingIntervalMs")]
        public int PollingIntervalMs { set; get; }

        [JsonProperty("userId")]
        public string UserId { set; get; }

        [JsonProperty("scribanMeterFromHome")]
        public string ScribanMeterFromHome { set; get; }

        [JsonProperty("scribanLocationName")]
        public string ScribanLocationName { set; get; }

        [JsonProperty("home")]
        public StatusLocation Home { set; get; }

        [JsonProperty("locations")]
        public List<StatusLocation> Locations { set; get; }
    }
}
