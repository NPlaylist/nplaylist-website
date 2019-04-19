using NPlaylist.Models.Entity;

namespace NPlaylist.Managers.TagProvider
{
    public interface IAudioTagsProvider
    {
        AudioMeta GetTags(string uploadedFilePath);
    }
}
