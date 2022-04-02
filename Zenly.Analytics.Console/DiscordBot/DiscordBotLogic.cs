using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aijkl.Zenly.APIClient;
using Discord;
using Discord.WebSocket;
using Scriban;
using Zenly.Analytics.Console.Discord;
using Zenly.Analytics.Console.Settings;

namespace Zenly.Analytics.Console.DiscordBot
{
    internal class BusinessLogic : IDisposable
    {
        private DiscordSocketClient _socketClient;
        private readonly DiscordBotSettings _discordBotSettings;

        public event Action DiscordConnected;
        public event Action<Exception> DiscordDisconnected;
        public event Action ZenlyApiOk;
        public event Action<ZenlyApiException> ZenlyApiError;
        public event Action<Exception> CanIgnoreException;

        internal BusinessLogic(DiscordBotSettings discordBotSettings)
        {
            _discordBotSettings = discordBotSettings;
            _socketClient = new DiscordSocketClient();
            _socketClient.LoginAsync(TokenType.Bot, _discordBotSettings.Token).Wait();
            _socketClient.StartAsync().Wait();
            _socketClient.Connected += () =>
            {
                OnDiscordConnected();
                return Task.CompletedTask;
            };
            _socketClient.Disconnected += exception =>
            {
                OnDiscordDisconnected(exception);
                return Task.CompletedTask;
            };
            
        }

        internal void StartMainLogic(CancellationToken cancellationToken)
        {
            using var zenlyApiClient = new ZenlyApiClient();
            CachedLocations cachedLocations = new CachedLocations();
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    List<UserLocation> userLocations = _discordBotSettings.Tokens.Select(x =>
                    {
                        string[] userIds = _discordBotSettings.Users.Where(y => y.TokenId == x.Id).Select(y => y.ZenlyId).ToArray();
                        var temp =  zenlyApiClient.WidgetClient.FetchUsersLocationAsync(userIds, x.Value).GetAwaiter().GetResult().ToList();
                        OnZenlyApiOk();
                        return temp;
                    }).SelectMany(x => x).ToList();

                    foreach (var userLocation in userLocations)
                    {
                        User user = _discordBotSettings.Users.First(x => x.ZenlyId == userLocation.UserId);
                        InspectionLocation withinRangeInspectLocation = _discordBotSettings.InspectionLocations.FirstOrDefault(x => _discordBotSettings.ToleranceMeter >= x.GetDistanceMeter(userLocation));

                        if (withinRangeInspectLocation == null)
                        {
                            InspectionLocation inspectionLocation = cachedLocations.GetOrDefault(userLocation.UserId);
                            if (inspectionLocation != null)
                            {
                                SendLeaveMessage(user, inspectionLocation);
                                cachedLocations.Remove(user);
                            }
                            continue;
                        }

                        var cachedInspectionLocation = cachedLocations.GetOrDefault(userLocation.UserId);
                        if (cachedInspectionLocation == null)
                        {
                            cachedLocations.AddOrUpdate(user, withinRangeInspectLocation);
                            SendArrivalMessage(user, withinRangeInspectLocation);
                        }
                    }
                }
                catch (ZenlyApiException exception)
                {
                    OnZenlyApiError(exception);
                }
                catch (Exception exception)
                {
                    OnCanIgnoreException(exception);
                }
                finally
                {
                    Thread.Sleep(_discordBotSettings.PollingIntervalMs);
                }
            }
        }
        internal void SendArrivalMessage(User user, InspectionLocation inspectionLocation)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.Title = user.Name;
            embedBuilder.ThumbnailUrl = user.ProfileUrl;
            embedBuilder.Description = Template.Parse(_discordBotSettings.ScribanArrival).Render(new { locationName = inspectionLocation.LocationName });
            _socketClient.GetGuild(user.NotificationChannel.GuildId).GetTextChannel(user.NotificationChannel.ChannelId).SendMessageAsync(string.Empty, embed: embedBuilder.Build()).GetAwaiter().GetResult();
        }
        internal void SendLeaveMessage(User user, InspectionLocation inspectionLocation)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.Title = user.Name;
            embedBuilder.ThumbnailUrl = user.ProfileUrl;
            embedBuilder.Description = Template.Parse(_discordBotSettings.ScribanLeave).Render(new { locationName = inspectionLocation.LocationName });
            _socketClient.GetGuild(user.NotificationChannel.GuildId).GetTextChannel(user.NotificationChannel.ChannelId).SendMessageAsync(string.Empty, embed: embedBuilder.Build()).GetAwaiter().GetResult();
        }
        protected virtual void OnDiscordConnected()
        {
            DiscordConnected?.Invoke();
        }
        protected virtual void OnDiscordDisconnected(Exception e)
        {
            DiscordDisconnected?.Invoke(e);
        }
        protected virtual void OnZenlyApiOk()
        {
            ZenlyApiOk?.Invoke();
        }
        protected virtual void OnZenlyApiError(ZenlyApiException obj)
        {
            ZenlyApiError?.Invoke(obj);
        }
        protected virtual void OnCanIgnoreException(Exception obj)
        {
            CanIgnoreException?.Invoke(obj);
        }
        public void Dispose()
        {
            _socketClient?.Dispose();
            _socketClient = null;
        }
    }
}
