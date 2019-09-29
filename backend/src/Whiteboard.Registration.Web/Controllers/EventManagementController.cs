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
                    Guid.NewGuid(),
                    new EventTitle("Carlie's Amazing Event"),
                    new EventDescription("a wonderful event"),
                    new EventLocation("Stacktrace Headquarters"),
                    new EventStartDate(new DateTime(2019, 09,30)),
                    new EventEndDate(new DateTime(2019, 10, 20)),
                    new RegistrationOpenDate(new DateTime(2019, 09, 22)),
                    new RegistrationCloseDate(new DateTime(2019, 09, 29))),

                new EventManagementModel(
                    Guid.NewGuid(),
                    new EventTitle("Another Great Thingy Event"),
                    new EventDescription("here is the description"),
                    new EventLocation("New Shanghai"),
                    new EventStartDate(new DateTime(2020, 01, 13)),
                    new EventEndDate(new DateTime(2020, 02, 01)),
                    new RegistrationOpenDate(new DateTime(2019, 12, 13)),
                    new RegistrationCloseDate(new DateTime(2020, 01, 07))),
            };


        // GET /v1/events/
        [HttpGet]
        public ActionResult<IEnumerable<EventManagementDto>> Get()
        {
            var listOfEvents = new List<EventManagementDto>();

            foreach (var singleEvent in Events)
            {
                listOfEvents.Add(new EventManagementDto{
                    Id  = singleEvent.Id,
                    Title = singleEvent.Title.Value
                });
            }

            Console.WriteLine(Events.Count);

            return listOfEvents;
        }

        // GET /v1/events/{id}
        [HttpGet("{id}")]
        public ActionResult<EventManagementModel> Get(int id)
        {
            // return Events[id].Title.Value.ToString();
            return Events[id];
        }

        // POST /v1/events/
        [HttpPost]
        // public void Post([FromBody] string value)
        public void Post(EventManagementModel value)
        {
            // create a new event
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
        [HttpPut("{id}")]
        // public void Put(int id, [FromBody] EventManagementModel value)
        public void Put(int id, EventManagementModel value)
        {
            if (value.Title != (default(EventTitle)))
            {
                Events[id].Title = value.Title;
            }
            if (value.Description != (default(EventDescription)))
            {
            Events[id].Description = value.Description;
            }
            if (value.EventLocation != (default(EventLocation)))
            {
            Events[id].EventLocation = value.EventLocation;
            }
            if (value.EventStartDate != (default(EventStartDate)))
            {
            Events[id].EventStartDate = value.EventStartDate;
            }
            if (value.EventEndDate != (default(EventEndDate)))
            {
            Events[id].EventEndDate = value.EventEndDate;
            }
            if (value.RegistrationOpenDate != (default(RegistrationOpenDate)))
            {
            Events[id].RegistrationOpenDate = value.RegistrationOpenDate;
            }
            if (value.RegistrationCloseDate != default(RegistrationCloseDate))
            {
            Events[id].RegistrationCloseDate = value.RegistrationCloseDate;
            }
        }

        // DELETE /v1/events/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Events.RemoveAt(id);
        }
    }
}
