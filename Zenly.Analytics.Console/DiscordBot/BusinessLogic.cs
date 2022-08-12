using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aijkl.Zenly.APIClient;
using Discord;
using Discord.WebSocket;
using Scriban;
using Zenly.Analytics.Console.Discord;
using Zenly.Analytics.Console.Settings;
// ReSharper disable once AccessToDisposedClosure

namespace Zenly.Analytics.Console.DiscordBot
{
    internal class BusinessLogic : IDisposable
    {
        private readonly DiscordSocketClient _socketClient;
        private readonly NotificationCommandSettings _discordBotSettings;
        private bool _disposed;

        public event Action? DiscordConnected;
        public event Action<Exception>? DiscordDisconnected;
        public event Action? ZenlyApiOk;
        public event Action<ZenlyApiException>? ZenlyApiError;
        public event Action<Exception>? CanIgnoreException;
        internal BusinessLogic(NotificationCommandSettings discordBotSettings)
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
            var locationHolder = new LocationHolder();
            while (true)
            {
                if(_socketClient.ConnectionState != ConnectionState.Connected) continue;

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    foreach (var tokenUserMap in _discordBotSettings.Users.GroupBy(x => x.TokenId))
                    {
                        var tokenValue = _discordBotSettings.Tokens.First(x => x.Id == tokenUserMap.Key);
                        var locations = zenlyApiClient.WidgetClient.FetchUsersLocationAsync(tokenUserMap.Select(x => x.ZenlyId).ToArray(), tokenValue.Value).Result;
                        OnZenlyApiOk();

                        foreach (var userLocation in locations)
                        {
                            var user = _discordBotSettings.Users.First(x => x.ZenlyId == userLocation.UserId);

                            var namedLocation = _discordBotSettings.InspectionLocations.FirstOrDefault(x => _discordBotSettings.ToleranceMeter >= x.GetDistanceMeter(userLocation));
                            var cachedLocation = locationHolder.TryGet(user.ZenlyId);
                            if (namedLocation != null)
                            {
                                if (cachedLocation != null)
                                {
                                    SendLeaveMessage(user, cachedLocation);
                                    SendArrivalMessage(user, namedLocation);
                                }
                                else
                                {
                                    SendArrivalMessage(user, namedLocation);
                                }
                            }
                            else if(cachedLocation?.InspectionLocation != null)
                            {
                                SendLeaveMessage(user, cachedLocation);
                            }

                            locationHolder.AddOrUpdate(user, new CachedLocation(namedLocation, DateTime.Now, userLocation));
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
            var embedBuilder = new EmbedBuilder
            {
                Title = user.Name,
                ThumbnailUrl = user.ProfileUrl,
                Description = Template.Parse(_discordBotSettings.ScribanArrival).Render(new { locationName = inspectionLocation.LocationName })
            };
            _socketClient.GetGuild(user.NotificationChannel.GuildId).GetTextChannel(user.NotificationChannel.ChannelId).SendMessageAsync(string.Empty, embed: embedBuilder.Build()).GetAwaiter().GetResult();
        }
        internal void SendLeaveMessage(User user, CachedLocation cachedLocation)
        {
            var embedBuilder = new EmbedBuilder
            {
                Title = user.Name,
                ThumbnailUrl = user.ProfileUrl,
                Description = Template.Parse(_discordBotSettings.ScribanLeave).Render(new { locationName = cachedLocation.InspectionLocation?.LocationName ?? string.Empty })
            };
            embedBuilder.AddField(new EmbedFieldBuilder
            {
                Name = nameof(TimeSpan.TotalMinutes),
                Value = (int)(DateTime.Now - cachedLocation.StartDateTime).TotalMinutes
            });
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
            if (_disposed) return;

            _socketClient.Dispose();
            _disposed = true;
        }
    }
}
