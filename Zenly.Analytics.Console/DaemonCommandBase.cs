using System.Collections.Generic;
using Aijkl.Zenly.APIClient;
using Zenly.Analytics.ConsoleApp;
using Zenly.Analytics.DataBase;

namespace Zenly.Analytics.Console
{
    internal abstract class DaemonCommandBase
    {
        internal DaemonCommandBase(AppSettings appSettings)
        {
            AppSettings = appSettings;
        }
        protected AppSettings AppSettings { get; }
        internal abstract string CommandName { get; }
        internal abstract int ValueChanged(List<UserLocation> userLocations);
    }
}
