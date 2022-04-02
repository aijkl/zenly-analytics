using Newtonsoft.Json;

namespace Zenly.Analytics.Console.DiscordBot
{
    internal class Token
    {
        [JsonProperty("id")]
        public string Id { set; get; }

        [JsonProperty("value")]
        public string Value { set; get; }

        [JsonProperty("summary")]
        public string Summary { set; get; }
    }
}
