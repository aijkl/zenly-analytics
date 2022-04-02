using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Aijkl.Zenly.Analytics.ConsoleApp.Helper;
using Aijkl.Zenly.APIClient;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Spectre.Console.Cli;
using Zenly.Analytics.ConsoleApp;
using Zenly.Analytics.DataBase;
using Zenly.Analytics.DataBase.Entities;

namespace Zenly.Analytics.Console.Commands
{
    internal class DataBaseCommand : Command
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

            using var zenlyApiClient = new ZenlyApiClient();
            while (true)
            {
                try
                {
                    using var analyticsDbContext = new AnalyticsDbContext(appSettings.ConnectionString);
                    if (analyticsDbContext.UserEntities.AsNoTracking().Any() is false)
                    {
                        AnsiConsoleHelper.WrapMarkupLine(appSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.DaemonUserNotExits)));
                        continue;
                    }

                    List<UserLocation> cachedLocations = new List<UserLocation>();
                    foreach (TokenEntity tokenEntity in analyticsDbContext.TokenEntities.AsNoTracking())
                    {
                        IEnumerable<UserLocation> userLocations = zenlyApiClient.WidgetClient.FetchUsersLocationAsync(analyticsDbContext.UserEntities.AsNoTracking().Where(x => x.TokenId == tokenEntity.Id).Select(x => x.Id), tokenEntity.Value).Result;
                        AnsiConsoleHelper.WrapMarkupLine(appSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.ZenlyAPIOk)));

                        cachedLocations = cachedLocations.Concat(userLocations).ToList();
                        foreach (var userLocation in userLocations)
                        {
                            analyticsDbContext.LocationEntities.Add(new LocationEntity()
                            {
                                Latitude = userLocation.Latitude,
                                Longitude = userLocation.Longitude,
                                UserId = userLocation.UserId
                            });
                            analyticsDbContext.SaveChanges();
                        }
                    }
                }
                catch (ZenlyApiException exception)
                {
                    AnsiConsoleHelper.WrapMarkupLine(appSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.ZenlyError)));
                    AnsiConsole.WriteException(exception);
                }
                catch (Exception exception)
                {
                    AnsiConsoleHelper.WrapMarkupLine(appSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.GeneralUnexpected)));
                    AnsiConsole.WriteException(exception);
                }
                finally
                {
                    Thread.Sleep(appSettings.DataBaseCommand.PollingIntervalMs);
                }
            }
        }
    }
}
