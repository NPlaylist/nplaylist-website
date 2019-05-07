using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.DbModels;
using NPlaylist.Persistence.Tests.CrudRepository.Stubs;

namespace NPlaylist.Persistence.Tests.CrudRepository
{
    public class StubDbContextBuilder
    {
        private readonly DbContextOptions<StubDbContext> _dbOptions;
        private readonly List<StubDbModel> _models = new List<StubDbModel>();

        public StubDbContextBuilder(DbContextOptions<StubDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public StubDbContext Build()
        {
            using (var context = new StubDbContext(_dbOptions))
            {
                context.StubDbModels.AddRange(_models);
                _models.Clear();
                context.SaveChanges();
            }

            return new StubDbContext(_dbOptions);
        }

        public StubDbContextBuilder With(params StubDbModel[] models)
        {
            _models.AddRange(models);
            return this;
        }
    }
}
