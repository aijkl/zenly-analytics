using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zenly.Analytics.DataBase.Entities;
using Zenly.Analytics.DataBase.Expansions;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Zenly.Analytics.DataBase
{
    public sealed class AnalyticsDbContext : DbContext
    {
        private readonly string _connectionString;
        public AnalyticsDbContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }
        public DbSet<UserEntity> UserEntities { set; get; }
        public DbSet<TokenEntity> TokenEntities { set; get; }
        public DbSet<LocationEntity> LocationEntities { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                var createdOn = entry.SafeGetProperty("CreatedAt");
                var updatedOn = entry.SafeGetProperty("UpdatedAt");

                if (entry.State == EntityState.Added && createdOn != null)
                {
                    createdOn.CurrentValue = DateTime.UtcNow;
                }

                if (updatedOn != null)
                {
                    updatedOn.CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                var createdOn = entry.SafeGetProperty("CreatedAt");
                var updatedOn = entry.SafeGetProperty("UpdatedAt");

                if (entry.State == EntityState.Added && createdOn != null)
                {
                    createdOn.CurrentValue = DateTime.UtcNow;
                }

                if (updatedOn != null)
                {
                    updatedOn.CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}