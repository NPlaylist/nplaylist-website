using System.Linq;
using NPlaylist.Models.Entity;

namespace NPlaylist.Wrappers.TagWrapper
{
    public class TagLibWrapper : ITagWrapper
    {
        public AudioMeta Create(string uploadedFilePath)
        {
            var tagLibFile = TagLib.File.Create(uploadedFilePath);

            return new AudioMeta
            {
                Title = tagLibFile.Tag.Title,
                Album = tagLibFile.Tag.Album,
                Author = tagLibFile.Tag.AlbumArtists.FirstOrDefault()
            };
        }
    }
}
