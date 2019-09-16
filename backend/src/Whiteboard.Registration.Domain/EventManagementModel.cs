﻿using System;

namespace Whiteboard.Registration.Domain
{
    public class EventTitle : TinyType<string>
    {
        public EventTitle(string value) : base(value){}
    }

    // todo @Carlie tedium...

    public class EventManagementModel
    {
        public EventTitle Title { get; }
        public string Description { get; }
        public string EventLocation { get; }

        // todo @Carlie spliting these dates into
        // their own type, e.g. EventTiming
        // How can we make testing things that use DateTimes
        // resilient/reliable?
        public DateTime EventStartDate { get; }
        public DateTime EventEndDate { get; }
        public DateTime RegistrationOpenDate { get; }
        public DateTime RegistrationCloseDate { get; }

        public EventManagementModel(
            EventTitle title,
            string description,
            string eventLocation,
            DateTime eventStartDate,
            DateTime eventEndDate,
            DateTime registrationOpenDate,
            DateTime registrationCloseDate
        )
        {
            Title = title;
            Description = description;
            EventLocation = eventLocation;
            EventStartDate = eventStartDate;
            EventEndDate = eventEndDate;
            RegistrationOpenDate = registrationOpenDate;
            RegistrationCloseDate = registrationCloseDate;
        }

        public EventManagementModel With(
            EventTitle eventTitle = null,
            string description = null,
            string eventLocation = null,
            DateTime? eventStartDate = null,
            DateTime? eventEndDate = null,
            DateTime? registrationOpenDate = null,
            DateTime? registrationCloseDate = null
        ) => new EventManagementModel(
            title: eventTitle ?? Title,
            description: description ?? Description,
            eventLocation: eventLocation ?? EventLocation,
            eventStartDate: eventStartDate ?? EventStartDate,
            eventEndDate: eventEndDate ?? EventEndDate,
            registrationOpenDate: registrationOpenDate ?? RegistrationOpenDate,
            registrationCloseDate: registrationCloseDate ?? RegistrationCloseDate
        );

    }
}
