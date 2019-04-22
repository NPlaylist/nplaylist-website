using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using NPlaylist.Persistance.Tests.CrudRepository.Stubs;

namespace NPlaylist.Persistance.Tests.CrudRepository
{
    public class SqlCrudRepositoryTests
    {
        [Fact]
        public void GetById_RetrievesAsExpected()
        {
            var dbOptions = new DbContextOptionsBuilder<StubDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetById_RetrievesAsExpected))
                .Options;

            using (var context = new StubDbContext(dbOptions))
            {
                context.StubDbModels.Add(new StubDbModel { Id = 42 });
                context.SaveChanges();
            }

            using (var context = new StubDbContext(dbOptions))
            {
                var repo = new StubCrudRepository(context);
                repo.GetById(42).Should().NotBeNull();
            }
        }

        [Fact]
        public void Add_AddsAsExpected()
        {
            var dbOptions = new DbContextOptionsBuilder<StubDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(Add_AddsAsExpected))
                .Options;
                
            using (var context = new StubDbContext(dbOptions))
            {
                var repo = new StubCrudRepository(context);
                repo.Add(new StubDbModel());
                repo.Save();
            }

            using (var context = new StubDbContext(dbOptions))
            {
                context.StubDbModels.Should().NotBeEmpty();
            }
        }

        [Fact]
        public void Remove_RemovesAsExpected()
        {
            var dbOptions = new DbContextOptionsBuilder<StubDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(Remove_RemovesAsExpected))
                .Options;

            var model = new StubDbModel();
            using (var context = new StubDbContext(dbOptions))
            {
                context.Add(model);
                context.SaveChanges();
            }

            using (var context = new StubDbContext(dbOptions))
            {
                var repo = new StubCrudRepository(context);
                repo.Remove(model);
                repo.Save();
            }

            using (var context = new StubDbContext(dbOptions))
            {
                context.StubDbModels.Should().BeEmpty();
            }
        }

        [Fact]
        public void Update_DataIsChanged_UpdatesTheSameModel()
        {
            var dbOptions = new DbContextOptionsBuilder<StubDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(Update_DataIsChanged_UpdatesTheSameModel))
                .Options;

            var model = new StubDbModel { Id = 42, Data = 0 };
            using (var context = new StubDbContext(dbOptions))
            {
                context.Add(model);
                context.SaveChanges();
            }

            model.Data = 1;
            using (var context = new StubDbContext(dbOptions))
            {
                var repo = new StubCrudRepository(context);
                repo.Update(model);
                repo.Save();
            }

            using (var context = new StubDbContext(dbOptions))
            {
                context.StubDbModels.Find(model.Id).Data.Should().Be(1);
            }
        }

        [Fact]
        public async Task SaveAsync_SavesAsExpected()
        {
            var dbOptions = new DbContextOptionsBuilder<StubDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(SaveAsync_SavesAsExpected))
                .Options;

            using (var context = new StubDbContext(dbOptions))
            {
                var repo = new StubCrudRepository(context);
                repo.Add(new StubDbModel());
                await repo.SaveAsync();
            }

            using (var context = new StubDbContext(dbOptions))
            {
                context.StubDbModels.Should().NotBeEmpty();
            }
        }
    }
}
