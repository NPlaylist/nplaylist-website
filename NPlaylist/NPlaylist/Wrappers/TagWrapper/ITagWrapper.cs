using NPlaylist.Models.Entity;

namespace NPlaylist.Wrappers.TagWrapper
{
    public interface ITagWrapper
    {
        AudioMeta Create(string uploadedFilePath);
    }
}
