using System.Collections.Generic;
using System.Linq;
using Zenly.Analytics.Console.Discord;
using Zenly.APIClient;

namespace Zenly.Analytics.Console.DiscordBot
{
    internal class CachedLocations
    {
        private readonly List<KeyValuePair<User, CachedLocation>> _userLocations;

        internal CachedLocations()
        {
            _userLocations = new List<KeyValuePair<User, CachedLocation>>();
        }

        internal bool IsStay(User user, Location location, double toleranceMeter)
        {
            return _userLocations.Any(x => user.ZenlyId == x.Key.ZenlyId && x.Value.GetDistanceMeter(location) >= toleranceMeter);
        }

        internal CachedLocation GetOrDefault(string userId)
        {
            return _userLocations.FirstOrDefault(x => x.Key.ZenlyId == userId).Value;
        }

        internal void Remove(User user)
        {
            _userLocations.Remove(_userLocations.FirstOrDefault(x => x.Key.ZenlyId == user.ZenlyId));
        }

        internal void AddOrUpdate(User user, CachedLocation inspectionLocation)
        {
            var cachedLocation = _userLocations.FirstOrDefault(x => x.Key.ZenlyId == user.ZenlyId);
            if (cachedLocation.Equals(default(KeyValuePair<User, CachedLocation>)))
            {
                _userLocations.Remove(cachedLocation);
            }
            _userLocations.Add(new KeyValuePair<User, CachedLocation>(user, inspectionLocation));
        }
    }
}
