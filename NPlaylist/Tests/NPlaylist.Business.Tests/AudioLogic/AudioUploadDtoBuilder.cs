using System;
using Microsoft.AspNetCore.Http;
using NPlaylist.Business.AudioLogic;
using NPlaylist.Models;
using NSubstitute;

namespace NPlaylist.Business.Tests.AudioLogic
{
    public class AudioUploadDtoBuilder
    {
        private AudioUploadDto _audioUploadDto;

        public AudioUploadDtoBuilder()
        {
            _audioUploadDto = new AudioUploadDto();
            var fileMock = Substitute.For<IFormFile>();
            _audioUploadDto.File = new FileModel(fileMock);
        }

        public AudioUploadDto Build()
        {
            return _audioUploadDto;
        }

        public AudioUploadDtoBuilder WithPath(string path)
        {
            _audioUploadDto.Path = path;
            return this;
        }

        public AudioUploadDtoBuilder WithPublisherId(Guid id)
        {
            _audioUploadDto.PublisherId = id;
            return this;
        }

        public AudioUploadDtoBuilder WithFile(IFormFile file)
        {
            _audioUploadDto.File = new FileModel(file);
            return this;
        }
    }
}