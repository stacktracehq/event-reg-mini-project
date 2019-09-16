using System;
using Xunit;
using Whiteboard.Registration.Domain;

namespace Whiteboard.Registration.Domain.Tests
{
    public class EventManagementModelTests
    {
        [Fact]
        public void Test()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var updatedTitle = eventModel.With(
                eventTitle: new EventTitle("special value")
            );

            Assert.Equal(new EventTitle("special value"), updatedTitle.Title);
        }
    }

    public static class EventManagementModelBuilder
    {
        public static EventManagementModel Build()
        {
            Console.WriteLine("\n\nIn the local one");
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
