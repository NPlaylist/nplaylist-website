using System;

namespace NPlaylist.Business.Audio
{
    public class AudioUploadDto
    {
        public IFile File { get; set; }
        public Guid PublisherId { get; set; }

        public string Path { get; set; }
    }
}