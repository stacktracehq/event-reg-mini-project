using System;

namespace Whiteboard.Registration.Domain
{
    public class EventManagementModel
    {
        public string Title { get; }
        public string Description { get; }
        public string EventLocation { get; }
        public DateTime EventStartDate { get; }
        public DateTime EventEndDate { get; }
        public DateTime RegistrationOpenDate { get; }
        public DateTime RegistrationCloseDate { get; }

        public EventManagementModel(string title, string description, string eventLocation, DateTime eventStartDate, DateTime eventEndDate, DateTime registrationOpenDate, DateTime registrationCloseDate)
        {
            Title = title;
            Description = description;
            EventLocation = eventLocation;
            EventStartDate = eventStartDate;
            EventEndDate = eventEndDate;
            RegistrationOpenDate = registrationOpenDate;
            RegistrationCloseDate = registrationCloseDate;
        }

    }
}
