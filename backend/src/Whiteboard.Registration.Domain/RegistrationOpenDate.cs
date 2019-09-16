using System;

namespace Whiteboard.Registration.Domain
{
    public class RegistrationOpenDate : TinyType<DateTime>
    {
        public RegistrationOpenDate(DateTime value) : base(value)
        {
        }
    }