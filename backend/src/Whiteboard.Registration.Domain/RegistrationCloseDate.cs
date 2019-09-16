using System;

namespace Whiteboard.Registration.Domain
{
    public class RegistrationCloseDate : TinyType<DateTime>
    {
        public RegistrationCloseDate(DateTime value) : base(value)
        {
        }
    }
}