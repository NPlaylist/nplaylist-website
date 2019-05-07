using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NPlaylist.Persistence.Tests.CrudRepository.Stubs;
using Xunit;

namespace NPlaylist.Persistence.Tests.CrudRepository
{
    public class SqlCrudRepositoryTests
    {
        [Fact]
        public void GetById_GetsElementAsExpected()
        {
            using (var connection = new DbConnection())
            {
                var dbContextBuilder = new StubDbContextBuilder(connection.DbOptions)
                    .With(new StubDbModel { Id = 42 });

                using (var context = dbContextBuilder.Build())
                {
                    var sut = new StubCrudRepository(context);
                    sut.GetById(42).Should().NotBeNull();
                }
            }
        }

        [Fact]
        public void GetById_NoEntries_ReturnsNull()
        {
            using (var connection = new DbConnection())
            {
                var dbContextBuilder = new StubDbContextBuilder(connection.DbOptions);
                using (var context = dbContextBuilder.Build())
                {
                    var sut = new StubCrudRepository(context);
                    sut.GetById(42).Should().BeNull();
                }
            }
        }

        [Fact]
        public async Task AddAsync_AddsAsExpected()
        {
            using (var connection = new DbConnection())
            {
                var dbContextBuilder = new StubDbContextBuilder(connection.DbOptions);

                using (var context = dbContextBuilder.Build())
                {
                    var sut = new StubCrudRepository(context);
                    await sut.AddAsync(new StubDbModel(), CancellationToken.None);
                    await sut.SaveAsync(CancellationToken.None);
                }

                using (var context = dbContextBuilder.Build())
                {
                    context.StubDbModels.Should().NotBeEmpty();
                }
            }
        }

        [Fact]
        public void Remove_RemovesAsExpected()
        {
            var model = new StubDbModel();
            using (var connection = new DbConnection())
            {
                var dbContextBuilder = new StubDbContextBuilder(connection.DbOptions)
                    .With(model);

                using (var context = dbContextBuilder.Build())
                {
                    var sut = new StubCrudRepository(context);
                    sut.Remove(model);
                    sut.Save();
                }

                using (var context = dbContextBuilder.Build())
                {
                    context.StubDbModels.Should().BeEmpty();
                }
            }
        }

        [Fact]
        public void Update_DataIsChanged_UpdatesTheSameModel()
        {
            var model = new StubDbModel { Id = 42, Data = 0 };
            using (var connection = new DbConnection())
            {
                var dbContextBuilder = new StubDbContextBuilder(connection.DbOptions)
                    .With(model);

                model.Data = 1;
                using (var context = dbContextBuilder.Build())
                {
                    var sut = new StubCrudRepository(context);
                    sut.Update(model);
                    sut.Save();
                }

                using (var context = dbContextBuilder.Build())
                {
                    context.StubDbModels.Find(model.Id).Data.Should().Be(1);
                }
            }
        }
    }
}
