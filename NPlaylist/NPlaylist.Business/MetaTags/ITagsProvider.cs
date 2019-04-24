using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Business.MetaTags
{
    public interface ITagsProvider
    {
        AudioMeta GetTags(string uploadedFilePath);
    }
}