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
        public void Post()
        {
            // create a new event
            Events.Add(new EventManagementModel(
                Guid.NewGuid(),
                new EventTitle("Another Event"),
                new EventDescription("Description here"),
                new EventLocation("Awesome Location"),
                new EventStartDate(new DateTime(2020,02,02)),
                new EventEndDate(new DateTime(2020,03,02)),
                new RegistrationOpenDate(new DateTime(2020,01,02)),
                new RegistrationCloseDate(new DateTime(2020,01,30))));

                Console.WriteLine(Events.Count);
        }

        // PUT /v1/events/{id}
        [HttpPut("{id}")]
        // public void Put(int id, [FromBody] EventManagementModel value)
        public void Put(int id, EventManagementModel value)
        {
            // Events[id] = new EventManagementModel(
            //     value.Id,
            //     value.Title,
            //     value.Description,
            //     value.EventLocation,
            //     value.EventStartDate,
            //     value.EventEndDate,
            //     value.RegistrationOpenDate,
            //     value.RegistrationCloseDate
            // );

            Events[id].Title = value.Title;
            Events[id].Description = value.Description;
            Events[id].EventLocation = value.EventLocation;
            Events[id].EventStartDate = value.EventStartDate;
            Events[id].EventEndDate = value.EventEndDate;
            Events[id].RegistrationOpenDate = value.RegistrationOpenDate;
            Events[id].RegistrationCloseDate = value.RegistrationCloseDate;
        }

        // DELETE /v1/events/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Events.RemoveAt(id);
        }
    }
}
