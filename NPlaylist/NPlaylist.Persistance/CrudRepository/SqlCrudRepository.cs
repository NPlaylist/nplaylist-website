using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NPlaylist.Persistance.CrudRepository
{
    public class SqlCrudRepository<TEntity, UKey> : ICrudRepository<TEntity, UKey>
        where TEntity: class
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

        public void Add(TEntity element)
        {
            _dbSet.Add(element);
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

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
