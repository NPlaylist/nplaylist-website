using System;

namespace NPlaylist.Infrastructure.System
{
    public interface IDateTimeWrapper
    {
        DateTime Now { get; }
    }
}