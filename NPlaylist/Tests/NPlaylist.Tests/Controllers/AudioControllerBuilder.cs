using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using NPlaylist.Business.AudioLogic;
using NPlaylist.Controllers;
using NSubstitute;

namespace NPlaylist.Tests.Controllers
{
    public class AudioControllerBuilder
    {
        private IAudioService _audioService;
        private IMapper _mapper;
        private IAuthorizationService _authService;

        public AudioControllerBuilder()
        {
            _audioService = Substitute.For<IAudioService>();
            _mapper = Substitute.For<IMapper>();
            _authService = Substitute.For<IAuthorizationService>();
        }

        public AudioControllerBuilder WithAudioService(IAudioService audioService)
        {
            _audioService = audioService;
            return this;
        }

        public AudioControllerBuilder WithMapper(IMapper mapper)
        {
            _mapper = mapper;
            return this;
        }

        public AudioControllerBuilder WithAuthService(IAuthorizationService authService)
        {
            _authService = authService;
            return this;
        }

        public AudioController Build()
        {
            return new AudioController(
                _authService,
                _audioService,
                _mapper);
        }
    }
}