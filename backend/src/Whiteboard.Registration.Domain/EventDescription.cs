using System;

namespace Whiteboard.Registration.Domain
{
    public class EventDescription : TinyType<string>
    {
        public EventDescription(string value) : base(value)
        {
        }
    }
}