using System;

namespace Whiteboard.Registration.Domain.Tiny
{
    public class RegistrationCloseDate : TinyType<DateTime>
    {
        public RegistrationCloseDate(DateTime value) : base(value)
        {
        }
    }
}
