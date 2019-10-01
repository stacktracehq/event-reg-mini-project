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

    // [Route("[controller]")]
    [Route("v1/events")]
    [ApiController]
    public class EventManagementController : ControllerBase
    {
        public static List<EventManagementModel> Events { get; set; }  = new List<EventManagementModel>()
            {
                new EventManagementModel(
                    new Guid("bcc3b1c4-e74c-4423-8c05-da1c2c8c5510"),
                    new EventTitle("Carlie's Amazing Event"),
                    new EventDescription("a wonderful event"),
                    new EventLocation("Stacktrace Headquarters"),
                    new EventStartDate(new DateTime(2019, 09,30)),
                    new EventEndDate(new DateTime(2019, 10, 20)),
                    new RegistrationOpenDate(new DateTime(2019, 09, 22)),
                    new RegistrationCloseDate(new DateTime(2019, 09, 29))),

                new EventManagementModel(
                    new Guid("4313f647-db66-42ce-b6da-8c659164dadf"),
                    new EventTitle("Another Great Event Thingy"),
                    new EventDescription("here is the description"),
                    new EventLocation("New Shanghai"),
                    new EventStartDate(new DateTime(2020, 01, 13)),
                    new EventEndDate(new DateTime(2020, 02, 01)),
                    new RegistrationOpenDate(new DateTime(2019, 12, 13)),
                    new RegistrationCloseDate(new DateTime(2020, 01, 07))),
            };


        // GET /v1/events/
        [HttpGet]
        public ActionResult<IEnumerable<EventManagementDto>> Get(string title)
        {
            return Events
                .Select(@event =>
                        new EventManagementDto {
                            Id = @event.Id,
                            Title = @event.Title.Value
                        })
                .Where(@event => title == null || @event.Title.ToLower().Contains(title.ToLower()))
                .ToList();
        }

        // GET /v1/events/{id}
        [HttpGet("{id}")]
        public ActionResult<EventManagementModel> Get(Guid id)
        {
            return Events.Single(@event => @event.Id == id);
        }

        // POST /v1/events/
        [HttpPost]
        public void Post(EventManagementModel value)
        {
            Events.Add(new EventManagementModel(
                Guid.NewGuid(),
                new EventTitle(value.Title.Value),
                new EventDescription(value.Description.Value),
                new EventLocation(value.EventLocation.Value),
                new EventStartDate(value.EventStartDate.Value),
                new EventEndDate(value.EventEndDate.Value),
                new RegistrationOpenDate(value.RegistrationOpenDate.Value),
                new RegistrationCloseDate(value.RegistrationCloseDate.Value)));

                Console.WriteLine(Events.Count);
        }

        // PUT /v1/events/{id}
        // This is actually what a patch does
        [HttpPut("{id}")]
        public void Put(Guid id, EventManagementModel value)
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

        }

        // DELETE /v1/events/{id}
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Events.Remove(Events.Single(@event => @event.Id == id));
        }
    }

}
