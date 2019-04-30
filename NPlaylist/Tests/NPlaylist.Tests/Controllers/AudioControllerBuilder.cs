using NPlaylist.Controllers;
using NPlaylist.Services.AudioService;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPlaylist.Tests.Controllers
{
    public class AudioControllerBuilder
    {
        private IAudioService _audioService;

        public AudioControllerBuilder()
        {
            _audioService = Substitute.For<IAudioService>();
        }
        public AudioControllerBuilder WithAudioService(IAudioService audioService)
        {
            _audioService = audioService;
            return this;
        }
        public AudioController Build()
        {
            return new AudioController(_audioService);
        }
    }
}
