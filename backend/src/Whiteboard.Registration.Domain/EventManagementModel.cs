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
        public EventDescription Description { get; }
        public EventLocation EventLocation { get; }

        // todo @Carlie spliting these dates into
        // their own type, e.g. EventTiming
        // How can we make testing things that use DateTimes
        // resilient/reliable?
        public EventStartDate EventStartDate { get; }
        public EventEndDate EventEndDate { get; }
        public RegistrationOpenDate RegistrationOpenDate { get; }
        public RegistrationCloseDate RegistrationCloseDate { get; }

        public EventManagementModel(
            EventTitle title,
            EventDescription description,
            EventLocation eventLocation,
            EventStartDate eventStartDate,
            EventEndDate eventEndDate,
            RegistrationOpenDate registrationOpenDate,
            RegistrationCloseDate registrationCloseDate
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
            EventDescription description = null,
            EventLocation eventLocation = null,
            EventStartDate? eventStartDate = null,
            EventEndDate? eventEndDate = null,
            RegistrationOpenDate? registrationOpenDate = null,
            RegistrationCloseDate? registrationCloseDate = null
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
