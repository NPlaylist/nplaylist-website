using NPlaylist.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Services.AudioService
{
    public interface IAudioService
    {
        Task<IEnumerable<AudioEntryViewModel>> GetAudioEntriesAsync(CancellationToken ct);
    }
}
