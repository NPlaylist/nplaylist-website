using AutoMapper;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Services.AudioService;

namespace NPlaylist.Tests.Services.AudioService
{
    public class AudioServiceImplBuilder
    {
        private IAudioEntriesRepository _audioRepo;
        private IMapper _mapper;

        public AudioServiceImpl Build()
        {
            return new AudioServiceImpl(_mapper, _audioRepo);
        }

        public AudioServiceImplBuilder WithAudioRepo(IAudioEntriesRepository audioEntriesRepository)
        {
            _audioRepo = audioEntriesRepository;
            return this;
        }

        public AudioServiceImplBuilder WithMapper(IMapper mapper)
        {
            _mapper = mapper;
            return this;
        }
    }
}
