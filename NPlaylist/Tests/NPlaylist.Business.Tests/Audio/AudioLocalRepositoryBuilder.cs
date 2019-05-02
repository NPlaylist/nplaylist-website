using NPlaylist.Business.LocalRepository;
using NPlaylist.Infrastructure.System;
using NSubstitute;

namespace NPlaylist.Business.Tests.Audio
{
    public class AudioLocalRepositoryBuilder
    {
        private IDirectoryWrapper _directoryWrapper;
        private IPathWrapper _pathWrapper;
        private IFileStreamFactory _streamFactory;
        public AudioLocalRepositoryBuilder()
        {
            _pathWrapper = Substitute.For<IPathWrapper>();
            _streamFactory = Substitute.For<IFileStreamFactory>();
            _directoryWrapper = Substitute.For<IDirectoryWrapper>();
        }

        public AudioLocalRepositoryImpl Build()
        {
            return new AudioLocalRepositoryImpl(_pathWrapper, _directoryWrapper, _streamFactory);
        }

        public AudioLocalRepositoryBuilder WithDirectoryWrapper(IDirectoryWrapper directoryWrapper)
        {
            _directoryWrapper = directoryWrapper;
            return this;
        }

        public AudioLocalRepositoryBuilder WithFileStreamFactory(IFileStreamFactory streamFactory)
        {
            _streamFactory = streamFactory;
            return this;
        }

        public AudioLocalRepositoryBuilder WithPathWrapper(IPathWrapper pathWrapper)
        {
            _pathWrapper = pathWrapper;
            return this;
        }
    }
}