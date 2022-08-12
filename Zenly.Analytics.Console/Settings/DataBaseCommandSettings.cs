using Newtonsoft.Json;

namespace Zenly.Analytics.Console
{
    internal class DataBaseCommandSettings
    {
        [JsonProperty("pollingIntervalMs", Required = Required.Always)]
        internal int PollingIntervalMs { set; get; }
    }
}
