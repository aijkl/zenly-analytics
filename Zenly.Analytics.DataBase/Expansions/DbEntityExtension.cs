using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Zenly.Analytics.DataBase.Expansions
{
    internal static class DbEntityExtension
    {
        public static PropertyEntry SafeGetProperty(this EntityEntry entry, string propertyName)
        {
            if (entry.CurrentValues.Properties.Any(x => x.Name == propertyName))
            {
                return entry.Property(propertyName);
            }

            return null;
        }
    }
}