using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Models;
using NPlaylist.Models.Audio;
using NPlaylist.ViewModels;

namespace NPlaylist.Services.AudioService
{
    public interface IAudioService
    {
        Task<AudioViewModel> GetAudioAsync(Guid id, CancellationToken ct);
        Task UpdateAudioAsync(AudioViewModel audioViewModel, CancellationToken ct);
        Task<PaginatedList<AudioEntryViewModel>> GetAudioEntriesRangeAsync(int page, int count, CancellationToken ct);
    }
}
