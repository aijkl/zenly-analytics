using Newtonsoft.Json;
using Zenly.APIClient;

namespace Zenly.Analytics.Console.Discord
{
    internal class InspectionLocation : Location
    {
        public InspectionLocation(string id, string locationName, double longitude, double latitude) : base(longitude, latitude)
        {
            Id = id;
            LocationName = locationName;
        }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("locationName")]
        public string LocationName { set; get; }
    }
}
