using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Audio> AudioEntries { get; set; }
        public virtual DbSet<AudioMeta> AudioMetaEntries { get; set; }
        public virtual DbSet<Playlist> PlaylistEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AudioPlaylists>()
                .HasKey(t => new { t.AudioId, t.PlaylistId });
        }
    }
}
