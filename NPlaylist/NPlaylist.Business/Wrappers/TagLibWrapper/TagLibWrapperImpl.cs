using TagLib;

namespace NPlaylist.Business.Wrappers.TagLibWrapper
{
    public class TagLibWrapperImpl : ITagLibWrapper
    {
        public File Create(string filePath)
        {
            return File.Create(filePath);
        }
    }
}