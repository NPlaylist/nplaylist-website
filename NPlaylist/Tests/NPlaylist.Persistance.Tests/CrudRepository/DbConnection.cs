using System;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.Tests.CrudRepository.Stubs;

namespace NPlaylist.Persistence.Tests.CrudRepository
{
    public class DbConnection : IDisposable
    {
        public DbConnection(string dbName = "dummy_db")
        {
            DbOptions = new DbContextOptionsBuilder<StubDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            EnsureDbCreated();
        }

        public DbContextOptions<StubDbContext> DbOptions { get; }

        public void Dispose()
        {
            EnsureDbDeleted();
        }

        private void EnsureDbCreated()
        {
            using (var dbContext = new StubDbContext(DbOptions))
            {
                dbContext.Database.EnsureCreated();
            }
        }

        private void EnsureDbDeleted()
        {
            using (var dbContext = new StubDbContext(DbOptions))
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
