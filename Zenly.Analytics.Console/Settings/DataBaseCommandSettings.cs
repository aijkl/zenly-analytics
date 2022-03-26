using Newtonsoft.Json;

namespace Zenly.Analytics.Console
{
    internal class DataBaseCommandSettings
    {
        [JsonProperty("pollingIntervalMs")]
        internal int PollingIntervalMs { set; get; }
    }
}
