using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Domain.Tiny;
using Whiteboard.Registration.Web.Services;

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
        private readonly IEventManagementRepo _repo;

        public EventManagementController(IEventManagementRepo repo) {
            _repo = repo;
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
