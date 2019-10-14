using System;
using System.Collections.Generic;
using System.Net;
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
        public async Task<ActionResult<IEnumerable<EventManagementDto>>> Get(string title)
        {
            var dtos = await _repo.GetAll(title);
            return new ActionResult<IEnumerable<EventManagementDto>>(dtos);
        }

        // GET /v1/events/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EventManagementModel>> Get(Guid id)
        {
                var dto = await _repo.Get(id);
                if (dto == null)
                    return NotFound();
                return new ActionResult<EventManagementModel>(dto);

        }

        // POST /v1/events/
        [HttpPost]
        public Task Post(EventManagementModel value)
        {
            return _repo.Add(value);
        }

        // PUT /v1/events/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, EventManagementModel value)
        {
            if (value.Id != id)
                return BadRequest();
            await _repo.Update(value);
            return Ok();
        }

        // DELETE /v1/events/{id}
        [HttpDelete("{id}")]
        public Task Delete(Guid id)
        {
            return _repo.Delete(id);
        }
    }
}
