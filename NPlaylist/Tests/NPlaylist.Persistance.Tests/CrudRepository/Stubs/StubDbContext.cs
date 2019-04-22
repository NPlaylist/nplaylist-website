using Microsoft.EntityFrameworkCore;

namespace NPlaylist.Persistance.Tests.CrudRepository.Stubs
{
    public class StubDbContext : DbContext
    {
        public StubDbContext(DbContextOptions<StubDbContext> options)
            : base(options)
        {
        }

        public DbSet<StubDbModel> StubDbModels { get; set; }
    }
}
