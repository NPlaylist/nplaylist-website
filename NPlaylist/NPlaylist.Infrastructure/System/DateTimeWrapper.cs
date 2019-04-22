using System;

namespace NPlaylist.Infrastructure.System
{
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime Now => DateTime.Now;
    }
}