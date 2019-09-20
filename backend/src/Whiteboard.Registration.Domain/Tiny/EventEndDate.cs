using System;

namespace Whiteboard.Registration.Domain.Tiny
{
    public class EventEndDate : TinyType<DateTime>
    {
        public EventEndDate(DateTime value) : base(value)
        {
        }
    }
}