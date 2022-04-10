using Aijkl.Zenly.Analytics.ConsoleApp.Helper;
using Spectre.Console;
using Spectre.Console.Cli;
using Zenly.Analytics.Console.Commands;

namespace Aijkl.Zenly.Analytics.ConsoleApp
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            CommandApp commandApp = new CommandApp();
            commandApp.Configure(x =>
            {
                x.AddCommand<NotificationCommand>("notification");
                x.AddCommand<StatusCommand>("status");
                x.SetApplicationName("zenly analytics");
                x.SetApplicationVersion("1.0");
                x.SetExceptionHandler(exception =>
                {
                    AnsiConsoleHelper.WrapMarkupLine(exception.Message,AnsiConsoleHelper.State.Failure);
                    AnsiConsole.WriteException(exception);
                });
            });
            return commandApp.Run(args);
        }
    }
}