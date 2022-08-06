using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zenly.Analytics.ConsoleApp.Expansions
{
    public static class DbSetExpansion
    {
        public static void AddOrUpdate<TEntity>(this DbSet<TEntity> dbSet, DbContext dbContext, string key, TEntity entity) where TEntity : class
        {
            var entityFromDb = dbSet.Find(key);
            if (entityFromDb == null)
            {
                dbSet.Add(entity);
            }
            else
            {
                dbContext.Entry(entityFromDb).CurrentValues.SetValues(entity);
            }
        }
        public static void AddOrUpdate<TEntity>(this DbSet<TEntity> dbSet, DbContext dbContext, IDictionary<string, TEntity> keyEntityMap) where TEntity : class
        {
            foreach (var keyEntity in keyEntityMap)
            {
                var entityFromDb = dbSet.Find(keyEntity.Key);
                if (entityFromDb == null)
                {
                    dbSet.Add(keyEntity.Value);
                }
                else
                {
                    dbContext.Entry(entityFromDb).CurrentValues.SetValues(keyEntity.Value);
                }
            }
        }
    }
}
