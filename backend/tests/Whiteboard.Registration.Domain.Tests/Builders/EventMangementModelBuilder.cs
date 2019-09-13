using System;
using Xunit;
using Whiteboard.Registration.Domain;

namespace Whiteboard.Registration.Domain.Tests
{
    public static class EventManagementModelBuilder
    {
        public static EventManagementModel Build()
        {
            return new EventManagementModel(
                new EventTitle("Carlies Super Duper Event"),
                "event description",
                "event location",
                new DateTime(2019, 9, 12),
                new DateTime(2019, 9, 12),
                new DateTime(2019, 9, 1),
                new DateTime(2019, 9, 5)
            );
        }
    }
}
