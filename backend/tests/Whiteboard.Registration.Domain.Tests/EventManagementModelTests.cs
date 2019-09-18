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
}
