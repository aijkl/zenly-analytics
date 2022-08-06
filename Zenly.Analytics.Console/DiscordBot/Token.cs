using Newtonsoft.Json;

namespace Zenly.Analytics.Console.DiscordBot
{
    internal class Token
    {
        public Token(string id, string value, string summary)
        {
            Id = id;
            Value = value;
            Summary = summary;
        }

        [JsonProperty("id")]
        public string Id { set; get; }

        [JsonProperty("value")]
        public string Value { set; get; }

        [JsonProperty("summary")]
        public string Summary { set; get; }
    }
}
