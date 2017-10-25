using Microsoft.EntityFrameworkCore;
using WorldWebServer.Models;

namespace WorldWebServer.DataAccess
{
    public class WorldSqliteDbContext : DbContext
    {
        public WorldSqliteDbContext(DbContextOptions<WorldSqliteDbContext> options)
            : base(options) { }

        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
    }

    public class WorldSqliteDbContextFactory
    {
        public static WorldSqliteDbContext Create(string connectionStirng)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorldSqliteDbContext>();
            optionsBuilder.UseSqlite(connectionStirng);
            var dbContext = new WorldSqliteDbContext(optionsBuilder.Options);
            return dbContext;
        }
    }
}
