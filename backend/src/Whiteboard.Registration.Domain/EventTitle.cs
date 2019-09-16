using System;

namespace Whiteboard.Registration.Domain
{
    public class EventTitle :  TinyType<string>
    {
        public EventTitle(string value) : base(value)
        {
        }
    }
}