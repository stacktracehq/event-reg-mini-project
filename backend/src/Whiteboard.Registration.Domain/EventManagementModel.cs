using System;
using Whiteboard.Registration.Domain.Tiny;

 namespace Whiteboard.Registration.Domain
{
    public class EventManagementModel
    {
        public Guid Id { get; }
        public EventTitle Title { get; set; }
        public EventDescription Description { get; set; }
        public EventLocation EventLocation { get; set;}

        // todo @Carlie spliting these dates into
        // their own type, e.g. EventTiming
        // How can we make testing things that use DateTimes
        // resilient/reliable?
        public EventStartDate EventStartDate { get; set;}
        public EventEndDate EventEndDate { get; set;}
        public RegistrationOpenDate RegistrationOpenDate { get; set;}
        public RegistrationCloseDate RegistrationCloseDate { get; set;}

        public EventManagementModel(
            Guid id,
            EventTitle title,
            EventDescription description,
            EventLocation eventLocation,
            EventStartDate eventStartDate,
            EventEndDate eventEndDate,
            RegistrationOpenDate registrationOpenDate,
            RegistrationCloseDate registrationCloseDate
        )
        {
            Id = id;
            Title = title;
            Description = description;
            EventLocation = eventLocation;
            EventStartDate = eventStartDate;
            EventEndDate = eventEndDate;
            RegistrationOpenDate = registrationOpenDate;
            RegistrationCloseDate = registrationCloseDate;
        }

        public EventManagementModel With(
            Guid? id = null,
            EventTitle eventTitle = null,
            EventDescription description = null,
            EventLocation eventLocation = null,
            EventStartDate eventStartDate = null,
            EventEndDate eventEndDate = null,
            RegistrationOpenDate registrationOpenDate = null,
            RegistrationCloseDate registrationCloseDate = null
        ) => new EventManagementModel(
            id: id ?? Id,
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
