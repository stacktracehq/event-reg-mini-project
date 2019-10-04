using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Domain.Tiny;
using Whiteboard.Registration.Web.Controllers;

namespace Whiteboard.Registration.Web.Services
{
    public class InMemoryEventManagementRepo : IEventManagementRepo
    {
        public List<EventManagementModel> Events = new List<EventManagementModel>();

        public Task Delete(Guid id)
        {
            Events.Remove(Events.Single(@event => @event.Id == id));
            return Task.CompletedTask;
        }

        public Task<EventManagementModel> Get(Guid id)
        {
            return Task.FromResult(Events.Single(@event => @event.Id == id));
        }

        public Task<IEnumerable<EventManagementDto>> GetAll(string title)
        {
            return Task.FromResult(
                Events
                    .Select(@event =>
                            new EventManagementDto {
                                Id = @event.Id,
                                Title = @event.Title.Value
                            })
                    .Where(@event => title == null || @event.Title.ToLower().Contains(title.ToLower())));
        }

        // put
        public Task Update(Guid id, EventManagementModel value)
        {
            var singleEvent = Events.Single(@event => @event.Id == id);

            if (value.Title != (default(EventTitle)))
            {
                singleEvent.Title = value.Title;
            }
            if (value.Description != (default(EventDescription)))
            {
                singleEvent.Description = value.Description;
            }
            if (value.EventLocation != (default(EventLocation)))
            {
                singleEvent.EventLocation = value.EventLocation;
            }
            if (value.EventStartDate != (default(EventStartDate)))
            {
                singleEvent.EventStartDate = value.EventStartDate;
            }
            if (value.EventEndDate != (default(EventEndDate)))
            {
                singleEvent.EventEndDate = value.EventEndDate;
            }
            if (value.RegistrationOpenDate != (default(RegistrationOpenDate)))
            {
                singleEvent.RegistrationOpenDate = value.RegistrationOpenDate;
            }
            if (value.RegistrationCloseDate != default(RegistrationCloseDate))
            {
                singleEvent.RegistrationCloseDate = value.RegistrationCloseDate;
            }

            return Task.CompletedTask;
        }

        // This is like a post
        public Task Update(EventManagementModel value)
        {
            Events.Add(
                new EventManagementModel(
                Guid.NewGuid(),
                new EventTitle(value.Title.Value),
                new EventDescription(value.Description.Value),
                new EventLocation(value.EventLocation.Value),
                new EventStartDate(value.EventStartDate.Value),
                new EventEndDate(value.EventEndDate.Value),
                new RegistrationOpenDate(value.RegistrationOpenDate.Value),
                new RegistrationCloseDate(value.RegistrationCloseDate.Value)));

                return Task.CompletedTask;
                // Console.WriteLine(Events.Count);
        }
    }

}
