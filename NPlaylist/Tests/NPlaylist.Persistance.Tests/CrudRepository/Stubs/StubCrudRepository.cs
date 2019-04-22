using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistance.CrudRepository;

namespace NPlaylist.Persistance.Tests.CrudRepository.Stubs
{
    public class StubCrudRepository : SqlCrudRepository<StubDbModel, int>
    {
        public StubCrudRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
