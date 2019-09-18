using System;

namespace Whiteboard.Registration.Domain
{
    public class EventEndDate : TinyType<DateTime>
    {
        public EventEndDate(DateTime value) : base(value)
        {
        }
    }
}