using NPlaylist.Business.LocalRepository;
using NPlaylist.Infrastructure.System;
using NSubstitute;

namespace NPlaylist.Business.Tests.AudioLogic
{
    public class AudioLocalRepositoryBuilder
    {
        private IDirectoryWrapper _directoryWrapper;
        private IPathWrapper _pathWrapper;
        private IFileStreamFactory _streamFactory;
        private IFileWrapper _fileWrapper;
        public AudioLocalRepositoryBuilder()
        {
            _pathWrapper = Substitute.For<IPathWrapper>();
            _streamFactory = Substitute.For<IFileStreamFactory>();
            _directoryWrapper = Substitute.For<IDirectoryWrapper>();
            _fileWrapper = Substitute.For<IFileWrapper>();
        }

        public AudioLocalRepositoryImpl Build()
        {
            return new AudioLocalRepositoryImpl(
                _pathWrapper,
                _directoryWrapper,
                _streamFactory,
                _fileWrapper);
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

        public AudioLocalRepositoryBuilder WithFileWrapper(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
            return this;
        }
    }
}