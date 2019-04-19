using NPlaylist.Models.Entity;
using NPlaylist.Wrappers.TagWrapper;

namespace NPlaylist.Managers.TagProvider
{
    public class AudioTagsProvider : IAudioTagsProvider
    {
        private readonly ITagWrapper _tagWrapper;

        public AudioTagsProvider(ITagWrapper tagWrapper)
        {
            _tagWrapper = tagWrapper;
        }
        public AudioMeta GetTags(string uploadedFilePath)
        {
            return _tagWrapper.Create(uploadedFilePath);
        }
    }
}
