using System;
using Whiteboard.Registration.Domain.Tiny;

namespace Whiteboard.Registration.Domain.Tests.Builders
{
    public static class EventManagementModelBuilder
    {
        public static EventManagementModel Build()
        {
            var dt = DateTime.Now;

            return new EventManagementModel(
                Guid.NewGuid(),
                new EventTitle("Carlies Super Duper Event"),
                new EventDescription("event description"),
                new EventLocation("event location"),
                new EventStartDate(DateTime.Today.AddDays(22).AddHours(9).AddMinutes(30)),
                new EventEndDate(DateTime.Today.AddDays(23).AddHours(9).AddMinutes(15)),
                new RegistrationOpenDate(new DateTime(2019, 9, 1)),
                new RegistrationCloseDate(new DateTime(2019, 9, 5))
            );
        }
    }
}
