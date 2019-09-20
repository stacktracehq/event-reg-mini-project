using System;

namespace Whiteboard.Registration.Domain.Tiny
{
    public class RegistrationOpenDate : TinyType<DateTime>
    {
        public RegistrationOpenDate(DateTime value) : base(value)
        {
        }
    }
}
