using System;
using Whiteboard.Registration.Domain.Tiny;

namespace Whiteboard.Registration.Domain.Tests.Builders
{
    public static class EventManagementModelBuilder
    {
        public static EventManagementModel Build()
        {
            return new EventManagementModel(
                new EventTitle("Carlies Super Duper Event"),
                new EventDescription("event description"),
                new EventLocation("event location"),
                new EventStartDate(new DateTime(2019, 9, 12)),
                new EventEndDate(new DateTime(2019, 9, 12)),
                new RegistrationOpenDate(new DateTime(2019, 9, 1)),
                new RegistrationCloseDate(new DateTime(2019, 9, 5))
            );
        }
    }
}
