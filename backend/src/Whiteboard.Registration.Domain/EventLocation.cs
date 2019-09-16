using System;

namespace Whiteboard.Registration.Domain
{
    public class EventLocation : TinyType<string>
    {
        public EventLocation(string value) : base(value)
        {
        }
    }