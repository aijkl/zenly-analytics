using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Aijkl.Zenly.Analytics.ConsoleApp.Helper;
using Aijkl.Zenly.APIClient;
using GeoCoordinatePortable;
using Spectre.Console;
using Spectre.Console.Cli;
using Zenly.Analytics.ConsoleApp;

namespace Zenly.Analytics.Console.Commands
{
    internal class DiscordBotCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            AppSettings appSettings;
            try
            {
                appSettings = AppSettings.LoadFromFile();
            }
            catch (Exception exception)
            {
                AnsiConsoleHelper.WrapMarkupLine(AppSettings.FileExceptionMessage, AnsiConsoleHelper.State.Failure);
                AnsiConsole.WriteException(exception);
                return 1;
            }

            appSettings.DiscordBotCommandSettings.NotificationRule

            try
            {
                var serializedLocations = userLocations.Select(x => new SerializedLocation(x.UserId, new GeoCoordinate(x.Latitude, x.Latitude), DateTime.Now));
                foreach (var serializedLocation in serializedLocations)
                {
                    var cachedLocation = _cachedLocations.FirstOrDefault(x => serializedLocation.UserId == x.UserId);
                    if (cachedLocation == null)
                    {
                        return 0;
                    }

                    if (cachedLocation.GeoCoordinate.GetDistanceTo(serializedLocation.GeoCoordinate) <= _appSettings.DiscordBotCommandSettings.ToleranceMeter)
                    {
                    }
                }
            }
            catch (ZenlyApiException exception)
            {
                AnsiConsoleHelper.WrapMarkupLine(AppSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.ZenlyError)));
                AnsiConsole.WriteException(exception);
            }
            catch (Exception exception)
            {
                AnsiConsoleHelper.WrapMarkupLine(AppSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.GeneralUnexpected)));
                AnsiConsole.WriteException(exception);
            }
            finally
            {
                Thread.Sleep(AppSettings.DataBaseCommandSettings.PollingIntervalMs);
            }
        }
    }
}
