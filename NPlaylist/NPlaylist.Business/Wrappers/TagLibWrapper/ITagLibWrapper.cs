namespace NPlaylist.Business.Wrappers.TagLibWrapper
{
    interface ITagLibWrapper
    {
        TagLib.File Create(string filePath);
    }
}
