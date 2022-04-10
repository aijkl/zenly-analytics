using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aijkl.Zenly.APIClient;
using Discord;
using Discord.WebSocket;
using Scriban;
using Zenly.Analytics.Console.Settings;

namespace Zenly.Analytics.Console.DiscordRpc
{
    internal class BusinessLogic
    {
        private readonly StatusCommandSettings _settings;
        public event Action DiscordConnected;
        public event Action<Exception> DiscordDisconnected;
        public event Action ZenlyApiOk;
        public event Action<ZenlyApiException> ZenlyApiError;
        public event Action<Exception> CanIgnoreException;
        internal BusinessLogic(StatusCommandSettings settings)
        {
            _settings = settings;
        }

        internal async Task StartThread(CancellationToken cancellationToken)
        {
            await using var discordSocketClient = new DiscordSocketClient();
            await discordSocketClient.LoginAsync(TokenType.Bot, _settings.Token);
            await discordSocketClient.StartAsync();
            var cancellationTokenSource = new CancellationTokenSource();
            discordSocketClient.Disconnected += exception =>
            {
                OnDiscordDisconnected(exception);
                return Task.CompletedTask;
            };
            discordSocketClient.Ready += () =>
            {
                cancellationTokenSource.Cancel();
                OnDiscordConnected();
                return Task.CompletedTask;
            };
            if (cancellationTokenSource.Token.IsCancellationRequested is false)
            {
                try
                {
                    await Task.Delay(10000, cancellationTokenSource.Token).WaitAsync(cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
            }

            using var zenlyApiClient = new ZenlyApiClient();
            var locations = _settings.Locations.Concat(new List<StatusLocation>() { _settings.Home }).ToArray();
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    UserLocation userLocation = await zenlyApiClient.WidgetClient.FetchUserLocationAsync(_settings.UserId, _settings.ZenlyToken).ConfigureAwait(false);
                    string locationName = locations.FirstOrDefault(x => x.GetDistanceMeter(userLocation) <= x.ToleranceMeter)?.LocationName;
                    string message;
                    if (string.IsNullOrEmpty(locationName))
                    {
                        message = await Template.Parse(_settings.ScribanMeterFromHome).RenderAsync(new { meter = (int)_settings.Home.GetDistanceMeter(userLocation) });
                    }
                    else
                    {
                        message = await Template.Parse(_settings.ScribanLocationName).RenderAsync(new { locationName });
                    }

                    await discordSocketClient.SetGameAsync(message);
                    OnZenlyApiOk();
                    Thread.Sleep(_settings.PollingIntervalMs);
                }
                catch (ZenlyApiException exception)
                {
                    OnZenlyApiError(exception);
                }
                catch (Exception exception)
                {
                    OnCanIgnoreException(exception);
                }
            }
        }

        protected virtual void OnDiscordConnected()
        {
            DiscordConnected?.Invoke();
        }
        protected virtual void OnDiscordDisconnected(Exception obj)
        {
            DiscordDisconnected?.Invoke(obj);
        }
        protected virtual void OnZenlyApiOk()
        {
            ZenlyApiOk?.Invoke();
        }
        protected virtual void OnCanIgnoreException(Exception obj)
        {
            CanIgnoreException?.Invoke(obj);
        }
        protected virtual void OnZenlyApiError(ZenlyApiException obj)
        {
            ZenlyApiError?.Invoke(obj);
        }
    }
}
