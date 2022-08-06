using System;
using Aijkl.Zenly.APIClient;

namespace Zenly.Analytics.Console.Discord
{
    internal record CachedLocation(InspectionLocation? InspectionLocation, DateTime StartDateTime, UserLocation UserLocation);
}
