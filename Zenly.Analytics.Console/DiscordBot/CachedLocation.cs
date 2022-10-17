using System;

namespace Zenly.Analytics.Console.Discord
{
    internal record CachedLocation(InspectionLocation InspectionLocation, DateTime StartDateTime) : InspectionLocation(InspectionLocation)
    {
        public bool ArrivalNotifySent { set; get; }
        public DateTime? LeaveDateTime { set; get; }
    }
}
