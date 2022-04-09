using System;

namespace Zenly.Analytics.Console.Discord
{
    internal record CachedLocation(InspectionLocation InspectionLocation, DateTime StartDateTime) : InspectionLocation(InspectionLocation);
}
