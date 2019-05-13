using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Persistence.CrudRepository
{
    public interface ICrudRepository<TEntity, UKey> where TEntity: class
    {
        TEntity GetById(UKey id);
        Task<TEntity> GetByIdAsync(UKey id, CancellationToken ct);

        Task AddAsync(TEntity element, CancellationToken ct);
        void Remove(TEntity element);
        void Update(TEntity element);

        void Save();
        Task SaveAsync(CancellationToken ct);
    }
}