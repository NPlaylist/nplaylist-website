using System;
using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Business.AudioLogic
{
    public interface IAudioService
    {
        Task UploadAudioAsync(AudioUploadDto uploadDto, CancellationToken ct);

        Task<Audio> GetAudioAsync(Guid id, CancellationToken ct);

        Task UpdateAudioAsync(Audio audio, CancellationToken ct);

        Task DeleteAudioAsync(Guid id, CancellationToken ct);

        Task<AudioPaginationDto> GetAudioPaginationAsync(int page, int count, CancellationToken ct);
    }
}