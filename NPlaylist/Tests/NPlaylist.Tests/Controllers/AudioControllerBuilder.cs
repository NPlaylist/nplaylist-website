using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using NPlaylist.Controllers;
using NPlaylist.Services.AudioService;
using NSubstitute;

namespace NPlaylist.Tests.Controllers
{
    public class AudioControllerBuilder
    {
        private IAudioService _audioService;
        private Business.Audio.IAudioService _audioServiceBl;
        private IMapper _mapper;
        private IAuthorizationService _authService;

        public AudioControllerBuilder()
        {
            _audioService = Substitute.For<IAudioService>();
            _audioServiceBl = Substitute.For<Business.Audio.IAudioService>();
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
                _audioServiceBl,
                _mapper);
        }
    }
}