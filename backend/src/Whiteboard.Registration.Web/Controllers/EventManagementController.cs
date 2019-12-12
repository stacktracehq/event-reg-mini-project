using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Web.Models;
using Whiteboard.Registration.Web.Services;

namespace Whiteboard.Registration.Web.Controllers
{
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
        public async Task<ActionResult> Post(EventManagementModel value)
        {
            try
            {
                Validation.validate(value);
                await _repo.Add(value);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return new JsonResult(ex)
                {
                    StatusCode = 400
                } as ActionResult;
            }
        }

        // PUT /v1/events/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, EventManagementModel value)
        {
            if (value.Id != id)
                return BadRequest();

            try
            {
                Validation.validate(value);
                await _repo.Update(value);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return new JsonResult(ex)
                {
                    StatusCode = 400
                } as ActionResult;
            }
        }

        // DELETE /v1/events/{id}
        [HttpDelete("{id}")]
        public Task Delete(Guid id)
        {
            return _repo.Delete(id);
        }
    }
}
