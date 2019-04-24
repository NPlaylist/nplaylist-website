using NPlaylist.Business.TagLibWrapper;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Business.MetaTags
{
    public class TagLibTagsProvider : ITagsProvider
    {
        private readonly ITagLibWrapper _tagLibWrapper;

        public TagLibTagsProvider(ITagLibWrapper tagLibWrapper)
        {
            _tagLibWrapper = tagLibWrapper;
        }

        public AudioMeta GetTags(string uploadedFilePath)
        {
            var tagFile = _tagLibWrapper.Create(uploadedFilePath);
            var audioMeta = new AudioMeta
            {
                Album = tagFile.Tag.Album,
                Author = tagFile.Tag.FirstPerformer,
                Title = tagFile.Tag.Title
            };

            return audioMeta;
        }
    }
}