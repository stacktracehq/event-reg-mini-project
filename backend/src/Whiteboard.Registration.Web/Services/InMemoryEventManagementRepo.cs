using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Web.Models;

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
            return Task.FromResult(Events.FirstOrDefault(@event => @event.Id == id));

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

        // Put
        public Task Update(EventManagementModel value)
        {
            var singleEvent = Events.SingleOrDefault(@event => @event.Id == value.Id);

            if (singleEvent != null)
                Events.Remove(singleEvent);

            Events.Add(value);
            return Task.CompletedTask;
        }

        // Post
        public Task Add(EventManagementModel value)
        {
            Events.Add(
                value.With(id: Guid.NewGuid())
                );

            return Task.CompletedTask;
        }
    }

}
