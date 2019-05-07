using System;

namespace NPlaylist.Persistence.Tests
{
    public static class GuidFactory
    {
        public static Guid MakeFromInt(int value)
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
