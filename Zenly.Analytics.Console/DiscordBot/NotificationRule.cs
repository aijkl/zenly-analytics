using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zenly.Analytics.Console.Discord
{
    internal class NotificationRule
    {
        [JsonProperty("zenlyId")]
        public string ZenlyId { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("profileUrl")]
        public string ProfileUrl { set; get; }

        [JsonProperty("tokenId")]
        public string TokenId { set; get; }

        [JsonProperty("inspectionLocationIds")]
        public List<string> InspectionLocationIds { set; get; }
    }
}
