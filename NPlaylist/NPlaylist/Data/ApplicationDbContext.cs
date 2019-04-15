using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Models.Entity;

namespace NPlaylist.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Audio> Audios { get; set; }
        public DbSet<AudioMeta> AudioMetas { get; set; }
    }
}
