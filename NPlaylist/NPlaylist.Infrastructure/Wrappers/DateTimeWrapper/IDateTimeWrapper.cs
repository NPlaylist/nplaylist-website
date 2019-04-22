using System;

namespace NPlaylist.Infrastructure.Wrappers.DateTimeWrapper
{
    public interface IDateTimeWrapper
    {
        DateTime Now { get; }
    }
}