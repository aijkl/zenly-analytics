using Newtonsoft.Json;
using Zenly.APIClient;

namespace Zenly.Analytics.Console.Discord
{
    internal class InspectionLocation : Location
    {
        [JsonProperty("locationName", Required = Required.Always)]
        public string LocationName { set; get; }
    }
}
