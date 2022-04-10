using Newtonsoft.Json;
using Zenly.APIClient;

namespace Zenly.Analytics.Console.DiscordRpc
{
    internal record StatusLocation([JsonProperty("locationName", Required = Required.Always)] string LocationName, [JsonProperty("toleranceMeter", Required = Required.Always)] int ToleranceMeter, double Longitude, double Latitude) : Location(Longitude, Latitude);
}
