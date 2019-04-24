using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.CrudRepository;

namespace NPlaylist.Persistence.Tests.CrudRepository.Stubs
{
    public class StubCrudRepository : SqlCrudRepository<StubDbModel, int>
    {
        public StubCrudRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
