using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Whiteboard.Registration.Domain;

namespace Whiteboard.Registration.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventManagementController : ControllerBase
    {
        // GET /eventmanagement
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // in here we display all the events
             return new string[] { "value1", "value2" };
        }

        // GET /eventmanagement/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            // this is where we would display the details of one event
             return "value";
        }

        // POST /eventmanagement
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // create a new event
        }

        // PUT /eventmanagement/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // update the event
        }

        // DELETE /eventmanagement/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // delete the individual event
        }
    }
}
