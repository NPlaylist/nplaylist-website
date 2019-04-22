using System;

namespace NPlaylist.Infrastructure.System
{
    public class GuidWrapper : IGuidWrapper
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}