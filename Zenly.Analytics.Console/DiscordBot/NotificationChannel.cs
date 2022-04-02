using Newtonsoft.Json;

namespace Zenly.Analytics.Console.DiscordBot
{
    internal class NotificationChannel
    {
        [JsonProperty("guildId")]
        internal ulong GuildId { set; get; }

        [JsonProperty("channelId")]
        internal ulong ChannelId { set; get; }
    }
}
