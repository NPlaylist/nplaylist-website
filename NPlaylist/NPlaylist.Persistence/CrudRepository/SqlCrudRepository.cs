using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Persistence.CrudRepository
{
    public class SqlCrudRepository<TEntity, UKey> : ICrudRepository<TEntity, UKey>
        where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public SqlCrudRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public TEntity GetById(UKey id)
        {
            return _dbSet.Find(id);
        }

        public Task<TEntity> GetByIdAsync(UKey id, CancellationToken ct)
        {
            return _dbSet.FindAsync(new object[] { id }, ct);
        }

        public Task AddAsync(TEntity element, CancellationToken ct)
        {
            return _dbSet.AddAsync(element, ct);
        }

        public void Remove(TEntity element)
        {
            _dbSet.Remove(element);
        }

        public void Update(TEntity element)
        {
            _dbSet.Update(element);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public Task SaveAsync(CancellationToken ct)
        {
            return _dbContext.SaveChangesAsync(ct);
        }
    }
}
