using GeoCoordinatePortable;

namespace Zenly.Analytics.Console.Discord
{
    internal class SerializedLocation
    {
        internal SerializedLocation(string userId,GeoCoordinate geoCoordinate)
        {
            UserId = userId;
            GeoCoordinate = geoCoordinate;
        }
        internal string UserId { set; get; }
        internal GeoCoordinate GeoCoordinate { set; get; }
    }
}
