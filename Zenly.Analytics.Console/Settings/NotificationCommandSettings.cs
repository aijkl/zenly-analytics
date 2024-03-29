﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Zenly.Analytics.Console.Discord;
using Zenly.Analytics.Console.DiscordBot;

namespace Zenly.Analytics.Console.Settings
{
    internal class NotificationCommandSettings
    {
        public NotificationCommandSettings(string token, int pollingIntervalMs, string scribanArrival, string scribanLeave, double toleranceMeter, List<User> users, List<Token> tokens, List<InspectionLocation> inspectionLocations)
        {
            Token = token;
            PollingIntervalMs = pollingIntervalMs;
            ScribanArrival = scribanArrival;
            ScribanLeave = scribanLeave;
            ToleranceMeter = toleranceMeter;
            Users = users;
            Tokens = tokens;
            InspectionLocations = inspectionLocations;
        }

        [JsonProperty("token", Required = Required.Always)]
        internal string Token { set; get; }

        [JsonProperty("pollingIntervalMs", Required = Required.Always)]
        internal int PollingIntervalMs { set; get; }

        [JsonProperty("scribanArrival", Required = Required.Always)]
        internal string ScribanArrival { set; get; }

        [JsonProperty("scribanLeave", Required = Required.Always)]
        internal string ScribanLeave { set; get; }

        /// <summary>
        /// 許容誤差
        /// </summary>
        [JsonProperty("toleranceMeter", Required = Required.Always)]
        internal double ToleranceMeter { set; get; }

        [JsonProperty("users", Required = Required.Always)]
        internal List<User> Users { set; get; }

        [JsonProperty("tokens", Required = Required.Always)]
        internal List<Token> Tokens { set; get; }

        [JsonProperty("inspectionLocations", Required = Required.Always)]
        internal List<InspectionLocation> InspectionLocations { set; get; }
    }
}
