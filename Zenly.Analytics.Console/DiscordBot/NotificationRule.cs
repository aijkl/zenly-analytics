using System.Collections.Generic;
using Newtonsoft.Json;
using Zenly.Analytics.Console.DiscordBot;

namespace Zenly.Analytics.Console.Discord
{
    internal class User
    {
        [JsonProperty("zenlyId", Required = Required.Always)]
        public string ZenlyId { set; get; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { set; get; }

        [JsonProperty("profileUrl")]
        public string ProfileUrl { set; get; }

        [JsonProperty("tokenId", Required = Required.Always)]
        public string TokenId { set; get; }

        [JsonProperty("notificationChannel", Required = Required.Always)]
        public NotificationChannel NotificationChannel { set; get; }
    }
}
