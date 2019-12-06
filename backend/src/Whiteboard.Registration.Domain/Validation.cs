using System;

namespace Whiteboard.Registration.Domain
{
    public class Validation
    {
        public static void validate(EventManagementModel value)
        {
            if (value.EventStartDate.Value < DateTime.Now)
            {
                throw new ArgumentException("Start date must not be before today.");
            }

            if (value.EventStartDate.Value > value.EventEndDate.Value)
            {
                throw new ArgumentException("The start date must be before the end date");
            }

            if (( value.EventEndDate.Value - value.EventStartDate.Value ).TotalHours < 1)
            {
                throw new ArgumentException("The event must be at least an hour long");
            }

            if(value.EventStartDate.Value.Minute % 15 != 0)
            {
                throw new ArgumentException("The event start time must be in any 15min increment");
            }

            if(value.EventEndDate.Value.Minute % 15 != 0)
            {
                throw new ArgumentException("The event end time must be in any 15min increment");
            }
        }
    }
}