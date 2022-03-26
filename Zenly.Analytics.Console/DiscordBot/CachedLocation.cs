using System;
using GeoCoordinatePortable;

namespace Zenly.Analytics.Console.Discord
{
    internal class SerializedLocation
    {
        internal SerializedLocation(string userId,GeoCoordinate geoCoordinate, DateTime timestamp)
        {
            UserId = userId;
            GeoCoordinate = geoCoordinate;
            Timestamp = timestamp;
        }
        internal string UserId { set; get; }
        internal GeoCoordinate GeoCoordinate { set; get; }
        internal DateTime Timestamp { get; set; }
    }
}
