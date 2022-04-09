using Newtonsoft.Json;
using Zenly.APIClient;

namespace Zenly.Analytics.Console.Discord
{
    internal record InspectionLocation(double Longitude, double Latitude, [JsonProperty("locationName", Required = Required.Always)] string LocationName) : Location(Longitude, Latitude);
}
