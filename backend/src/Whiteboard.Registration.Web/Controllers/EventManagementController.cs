using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Domain.Tiny;

namespace Whiteboard.Registration.Web.Controllers
{
    public class EventManagementDto {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }

    public interface IEventManagementRepo {

        Task<IEnumerable<EventManagementDto>> GetAll(string title);
        Task<EventManagementModel> Get(Guid id);
        Task Update(Guid id, EventManagementModel value);
        Task Update(EventManagementModel value);
        Task Delete(Guid id);
    }

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

    // [Route("[controller]")]
    [Route("v1/events")]
    [ApiController]
    public class EventManagementController : ControllerBase
    {
        private static readonly IEventManagementRepo _repo = CreateRepo();

        private static IEventManagementRepo CreateRepo() {
            var repo = new InMemoryEventManagementRepo();
            repo.Update(
                new EventManagementModel(
                new Guid("bcc3b1c4-e74c-4423-8c05-da1c2c8c5510"),
                new EventTitle("Carlie's Amazing Event"),
                new EventDescription("a wonderful event"),
                new EventLocation("Stacktrace Headquarters"),
                new EventStartDate(new DateTime(2019, 09,30)),
                new EventEndDate(new DateTime(2019, 10, 20)),
                new RegistrationOpenDate(new DateTime(2019, 09, 22)),
                new RegistrationCloseDate(new DateTime(2019, 09, 29))));
            repo.Update(
                 new EventManagementModel(
                new Guid("4313f647-db66-42ce-b6da-8c659164dadf"),
                new EventTitle("A great event"),
                new EventDescription("here is the description"),
                new EventLocation("New Shanghai"),
                new EventStartDate(new DateTime(2020, 01, 13)),
                new EventEndDate(new DateTime(2020, 02, 01)),
                new RegistrationOpenDate(new DateTime(2019, 12, 13)),
                new RegistrationCloseDate(new DateTime(2020, 01, 07)))
            );
            return repo;
        }

        public EventManagementController() {
        }


        // GET /v1/events/
        [HttpGet]
        public Task<IEnumerable<EventManagementDto>> Get(string title)
        {
            return _repo.GetAll(title);
        }

        // GET /v1/events/{id}
        [HttpGet("{id}")]
        public Task<EventManagementModel> Get(Guid id)
        {
            return _repo.Get(id);
        }

        // POST /v1/events/
        [HttpPost]
        public void Post(EventManagementModel value)
        {
            _repo.Update(value);
        }

        // PUT /v1/events/{id}
        // This is actually what a patch does
        [HttpPut("{id}")]
        public void Put(Guid id, EventManagementModel value)
        {
            _repo.Update(id, value);
        }

        // DELETE /v1/events/{id}
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repo.Delete(id);
        }
    }

}
