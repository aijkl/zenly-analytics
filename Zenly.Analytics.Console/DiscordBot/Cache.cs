// TODO Stateをファイルに書いて再起動しても状態を継続する
using System;
using System.IO;
using Newtonsoft.Json;
using Zenly.Analytics.Console.DiscordBot;

namespace Zenly.Analytics.Console
{
    internal class Cache
    {
        private Cache(LocationHolder cachedLocations)
        {
            CachedLocations = cachedLocations;
        }

        [JsonIgnore]
        internal static readonly string FileName = "discord-bot-cache.json";

        [JsonProperty("cachedLocations")]
        public LocationHolder CachedLocations { set; get; }

        internal static Cache LoadFromFile()
        {
            return JsonConvert.DeserializeObject<Cache>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), FileName))) ?? throw new ApplicationException("TODO message");
        }
        internal void SaveToFile()
        {
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), FileName), JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
