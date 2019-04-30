using AutoMapper;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Services.AudioService;
using NSubstitute;

namespace NPlaylist.Tests.Services.AudioService
{
    public class AudioServiceImplBuilder
    {
        private IMapper _mapper;
        private IAudioEntriesRepository _audioRepository;

        public AudioServiceImplBuilder()
        {
            //_mapper = new MapperBuilder().WithDefaultProfile().Build();
            _mapper = Substitute.For<IMapper>();
            _audioRepository = Substitute.For<IAudioEntriesRepository>();
        }

        public AudioServiceImplBuilder WithAudioRepository(IAudioEntriesRepository audioRepository)
        {
            _audioRepository = audioRepository;
            return this;
        }

        public AudioServiceImplBuilder WithAutoMapper(IMapper mapper)
        {
            _mapper = mapper;
            return this;
        }

        public AudioServiceImpl Build()
        {
            return new AudioServiceImpl(_mapper, _audioRepository);
        }
    }
}
