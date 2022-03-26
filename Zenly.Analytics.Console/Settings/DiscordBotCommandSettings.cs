using System.Collections.Generic;
using Newtonsoft.Json;
using Zenly.Analytics.Console.Discord;

namespace Zenly.Analytics.Console.Settings
{
    internal class DiscordBotCommandSettings
    {
        [JsonProperty("toleranceMeter")]
        internal double ToleranceMeter { set; get; }

        [JsonProperty("notificationRule")]
        internal NotificationRule NotificationRule { set; get; }

        [JsonProperty("inspectionLocations")]
        internal List<InspectionLocation> InspectionLocations { set; get; }
    }
}
