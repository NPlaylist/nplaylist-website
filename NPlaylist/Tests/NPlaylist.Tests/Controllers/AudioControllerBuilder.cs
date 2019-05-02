using NPlaylist.Controllers;
using NPlaylist.Services.AudioService;
using NSubstitute;

namespace NPlaylist.Tests.Controllers
{
    public class AudioControllerBuilder
    {
        private IAudioService _audioService;
        private Business.Audio.IAudioService _audioServiceBl;

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

        public AudioController Build()
        {
            return new AudioController(_audioService,_audioServiceBl);
        }
    }
}