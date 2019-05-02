using NPlaylist.Business.Audio;
using NPlaylist.Business.LocalRepository;
using NPlaylist.Business.MetaTags;
using NPlaylist.Business.Providers;
using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence.AudioEntries;
using NSubstitute;

namespace NPlaylist.Business.Tests.Audio
{
    public class AudioServiceBuilder
    {
        private IAudioEntriesRepository _audioEntriesRepository;
        private IDateTimeWrapper _dateTimeWrapper;
        private IAudioLocalRepository _localAudioRepository;
        private IPathProvider _pathProvider;
        private ITagsProvider _tagsProvider;

        public AudioServiceBuilder()
        {
            _audioEntriesRepository = Substitute.For<IAudioEntriesRepository>();
            _dateTimeWrapper = Substitute.For<IDateTimeWrapper>();
            _localAudioRepository = Substitute.For<IAudioLocalRepository>();
            _pathProvider = Substitute.For<IPathProvider>();
            _tagsProvider = Substitute.For<ITagsProvider>();
        }

        public AudioServiceImpl Build()
        {
            return new AudioServiceImpl(
                _pathProvider,
                _localAudioRepository,
                _audioEntriesRepository,
                _tagsProvider,
                _dateTimeWrapper);
        }

        public AudioServiceBuilder WithAudioEntriesRepo(IAudioEntriesRepository audioEntriesRepo)
        {
            _audioEntriesRepository = audioEntriesRepo;
            return this;
        }

        public AudioServiceBuilder WithDateTimeWrapper(IDateTimeWrapper dateTimeWrapper)
        {
            _dateTimeWrapper = dateTimeWrapper;
            return this;
        }

        public AudioServiceBuilder WithLocalRepo(IAudioLocalRepository audioLocalRepo)
        {
            _localAudioRepository = audioLocalRepo;
            return this;
        }

        public AudioServiceBuilder WithPathProvider(IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
            return this;
        }

        public AudioServiceBuilder WithTagsProvider(ITagsProvider tagsProvider)
        {
            _tagsProvider = tagsProvider;
            return this;
        }
    }
}