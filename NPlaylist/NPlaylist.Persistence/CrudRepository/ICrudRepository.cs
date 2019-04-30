using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Persistence.CrudRepository
{
    public interface ICrudRepository<TEntity, UKey> where TEntity : class
    {
        TEntity GetById(UKey id);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct);

        void Add(TEntity element);
        void Remove(TEntity element);
        void Update(TEntity element);

        void Save();
        Task SaveAsync();
    }
}
