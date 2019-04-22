using System.Threading.Tasks;

namespace NPlaylist.Persistance.CrudRepository
{
    public interface ICrudRepository<TEntity, UKey> where TEntity: class
    {
        TEntity GetById(UKey id);

        void Add(TEntity element);
        void Remove(TEntity element);
        void Update(TEntity element);

        void Save();
        Task SaveAsync();
    }
}
