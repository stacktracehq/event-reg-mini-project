using System;
using Whiteboard.Registration.Domain.Tests.Builders;
using Whiteboard.Registration.Domain.Tiny;
using Xunit;

namespace Whiteboard.Registration.Domain.Tests
{
    public class ValidationTests
    {

        [Fact]
        public void ModelValidates()
        {
            var eventModel = EventManagementModelBuilder.Build();
            Validation.validate(eventModel);
        }

        [Fact]
        public void TestStartDate_ModelHasInvalidStartDate()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var yesterday = new EventStartDate(DateTime.Now.AddDays(-1));
            var invalidStartDateModel = eventModel.With(
                eventStartDate: yesterday
            );

            Assert.Throws<ArgumentException>(() =>
                Validation.validate(invalidStartDateModel)
            );
        }

        [Fact]
        public void TestStartDate_StartDateIsAfterEndDate()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var twoDaysFromNow = new EventStartDate(DateTime.Today.AddDays(+2));
            var tomorrow = new EventEndDate(DateTime.Now.AddDays(+1));
            var invalidStartDateModel = eventModel.With(
                eventStartDate: twoDaysFromNow,
                eventEndDate: tomorrow
            );

            Assert.Throws<ArgumentException>(() =>
                Validation.validate(invalidStartDateModel)
            );
        }

        [Fact]
        public void TestStartDate_StartDateIsBeforeEndDate()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var tomorrow = new EventStartDate(DateTime.Today.AddDays(1));
            var twoDaysFromNow = new EventEndDate(DateTime.Today.AddDays(2));
            var validStartDateModel = eventModel.With(
                eventStartDate: tomorrow,
                eventEndDate: twoDaysFromNow
            );

            Validation.validate(validStartDateModel);
        }

        [Fact]
        public void TestStartDate_StartDateIsNotIncrementalBy15mins()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var startTimeNotIncrementalBy15mins = new EventStartDate(DateTime.Today.AddDays(1).AddHours(9).AddMinutes(13));
            var invalidStartTimeModel = eventModel.With(
                eventStartDate: startTimeNotIncrementalBy15mins
            );

            Assert.Throws<ArgumentException>(() =>
                Validation.validate(invalidStartTimeModel)
            );
        }

        [Fact]
        public void TestStartDate_StartDateEndsWith45mins()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var startTimeEndsWith45mins = new EventStartDate(DateTime.Today.AddDays(1).AddHours(9).AddMinutes(45));
            var validStartTimeModel = eventModel.With(
                eventStartDate: startTimeEndsWith45mins
            );

            Validation.validate(validStartTimeModel);
        }

        [Fact]
        public void TestEndDate_EndDateIsLessThanOneHourAfterStartDate()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var startDate = new EventStartDate(DateTime.Now.AddHours(+1));
            var endTimeIsLessThanOneHourAfterStartTime = new EventEndDate(DateTime.Now.AddHours(+1.5));
            var invalidEndTimeModel = eventModel.With(
                eventStartDate: startDate,
                eventEndDate: endTimeIsLessThanOneHourAfterStartTime
            );

            Assert.Throws<ArgumentException>(() =>
                Validation.validate(invalidEndTimeModel)
            );
        }

        [Fact]
        public void TestEndDate_EndDateIsOneHourAfterStartDate()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var startTime = new EventStartDate(DateTime.Today.AddDays(1).AddHours(+1));
            var endTimeOneHourAfterStartTime = new EventEndDate(DateTime.Today.AddDays(1).AddHours(+2));
            var eventModelWithValidTimes = eventModel.With(
                eventStartDate: startTime,
                eventEndDate: endTimeOneHourAfterStartTime
            );

            Validation.validate(eventModelWithValidTimes);
        }

        [Fact]
        public void TestEndDate_EndDateIsNotIncrementalBy15mins()
        {
            var eventModel = EventManagementModelBuilder.Build();
            var endTimeNotIncrementalBy15mins = new EventEndDate(DateTime.Today.AddDays(1).AddHours(9).AddMinutes(13));
            var invalidEndDateModel = eventModel.With(
                eventEndDate: endTimeNotIncrementalBy15mins
            );

            Assert.Throws<ArgumentException>(() =>
                Validation.validate(invalidEndDateModel)
            );
        }

    }
}
