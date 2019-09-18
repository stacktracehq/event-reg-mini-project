using System;

namespace Whiteboard.Registration.Domain
{
    public class EventStartDate : TinyType<DateTime>
    {
        public EventStartDate(DateTime value) : base(value)
        {
        }
    }
}