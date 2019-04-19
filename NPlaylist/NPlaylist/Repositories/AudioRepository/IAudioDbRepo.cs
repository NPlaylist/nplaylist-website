using System.Threading;
using NPlaylist.Models.Entity;
using System.Threading.Tasks;

namespace NPlaylist.Repositories.AudioRepository
{
    public interface IAudioDbRepo
    {
        void Add(Audio audio);
        Task SaveAsync(CancellationToken ct);
    }
}
