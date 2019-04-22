using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPlaylist.Persistance.DbModels;

namespace NPlaylist.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public ApplicationDbContext(IConfiguration config, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
        }

        public DbSet<Audio> AudioEntries { get; set; }
        public DbSet<AudioMeta> AudioMetaEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("ApplicationDbConnection"), options =>
            {
                options.MigrationsHistoryTable("__UsersMigrationsHistory", "Application");
            });
        }
    }
}