using System.Threading;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Data;
using NPlaylist.Models.Entity;
using System.Threading.Tasks;

namespace NPlaylist.Repositories.AudioRepository
{
    public class SqlAudioDbRepo : IAudioDbRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Audio> _audioDbSet;

        public SqlAudioDbRepo(ApplicationDbContext context)
        {
            _context = context;
            _audioDbSet = context.Set<Audio>();
        }

        public void Add(Audio audio)
        {
            _audioDbSet.Add(audio);
        }

        public async Task SaveAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
