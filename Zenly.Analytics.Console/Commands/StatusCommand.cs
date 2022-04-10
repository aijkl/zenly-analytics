using System;
using System.Threading;
using Aijkl.Zenly.Analytics.ConsoleApp.Helper;
using Spectre.Console;
using Spectre.Console.Cli;
using Zenly.Analytics.Console.DiscordRpc;
using Zenly.Analytics.ConsoleApp;

namespace Zenly.Analytics.Console.Commands
{
    internal class StatusCommand : Command
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

            var businessLogic = new BusinessLogic(appSettings.Status);
            businessLogic.DiscordConnected += () =>
            {
                AnsiConsoleHelper.WrapMarkupLine(appSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.DiscordConnected)), AnsiConsoleHelper.State.Success);
            };
            businessLogic.DiscordDisconnected += exception =>
            {
                AnsiConsoleHelper.WrapMarkupLine(appSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.DiscordDisconnected)), AnsiConsoleHelper.State.Failure);
                AnsiConsole.WriteException(exception);
            };
            businessLogic.CanIgnoreException += exception =>
            {
                AnsiConsole.WriteException(exception);
            };
            businessLogic.ZenlyApiOk += () =>
            {
                AnsiConsoleHelper.WrapMarkupLine(appSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.ZenlyAPIOk)), AnsiConsoleHelper.State.Success);
            };
            businessLogic.ZenlyApiError += exception =>
            {
                AnsiConsoleHelper.WrapMarkupLine(appSettings.LanguageDataSet.GetValue(nameof(LanguageDataSet.ZenlyError)), AnsiConsoleHelper.State.Failure);
                AnsiConsole.WriteException(exception);
            };
            businessLogic.StartThread(CancellationToken.None).Wait();
            return 0;
        }
    }
}
