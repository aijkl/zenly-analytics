using System.IO;
using Newtonsoft.Json;
using Aijkl.Zenly.APIClient;
using System.Collections.Generic;
using Zenly.Analytics.Console.DiscordBot;

namespace Zenly.Analytics.Console
{
    internal class Cache
    {
        private Cache()
        {
        }

        [JsonIgnore]
        internal static readonly string FileName = "discord-bot-cache.json";

        [JsonProperty("cachedLocations")]
        public CachedLocations CachedLocations { set; get; }

        internal static Cache LoadFromFile()
        {
            Cache appSettings = JsonConvert.DeserializeObject<Cache>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), FileName)));
            return appSettings;
        }
        internal void SaveToFile()
        {
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), FileName), JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
