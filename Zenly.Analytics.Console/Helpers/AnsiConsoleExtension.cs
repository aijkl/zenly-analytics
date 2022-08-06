using Spectre.Console;
using System.Collections.Generic;

namespace Aijkl.Zenly.Analytics.ConsoleApp.Helper
{
    internal static class AnsiConsoleHelper
    {
        internal enum State
        {
            Success,
            Failure,
            Info
        }
        internal static void WrapMarkup(string text, State state = State.Info)
        {
            var colorMap = new Dictionary<State, Color>();
            colorMap.Add(State.Success, Color.Green1);
            colorMap.Add(State.Info, Color.Purple3);
            colorMap.Add(State.Failure, Color.Red1);

            if (!colorMap.TryGetValue(state, out var color)) return;
            AnsiConsole.Markup($" [[[rgb({color.R},{color.G},{color.B})]]]{state}[/] {text}");
        }
        internal static void WrapMarkupLine(string text, State state = State.Info)
        {
            var colorMap = new Dictionary<State, Color>();
            colorMap.Add(State.Success, Color.Green1);
            colorMap.Add(State.Info, Color.Grey46);
            colorMap.Add(State.Failure, Color.Red1);

            if (!colorMap.TryGetValue(state, out var color)) return;
            AnsiConsole.MarkupLine($" [[[rgb({color.R},{color.G},{color.B})]{state}[/]]] {text}");
        }
    }
}
