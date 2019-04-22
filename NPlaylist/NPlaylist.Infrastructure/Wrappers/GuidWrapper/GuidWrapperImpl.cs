using System;

namespace NPlaylist.Infrastructure.Wrappers.GuidWrapper
{
    public class GuidWrapperImpl : IGuidWrapper
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
