using System;

namespace NPlaylist.Infrastructure.Wrappers.DateTimeWrapper
{
    public class DateTimeWrapperImpl : IDateTimeWrapper
    {
        public DateTime Now => DateTime.Now;
    }
}