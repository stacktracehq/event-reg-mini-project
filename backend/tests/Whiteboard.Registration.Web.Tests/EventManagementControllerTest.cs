using Xunit;
using Whiteboard.Registration.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Web.Controllers;
using System.Collections.Generic;
using Whiteboard.Registration.Domain.Tiny;
using System.Text;

namespace Whiteboard.Registration.Web.Tests
{
    public class EventManagementControllerTest
    {
        [Fact]
        public async Task PostAddsANewEventManagementModel()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var model = new EventManagementModel(
                new Guid("8b014e41-d74f-4031-a3d7-ae264144b989"),
                new EventTitle("Carlie's awesome party"),
                new EventDescription("a wonderful event"),
                new EventLocation("Stacktrace Headquarters"),
                new EventStartDate(new DateTime(2019, 09,30)),
                new EventEndDate(new DateTime(2019, 10, 20)),
                new RegistrationOpenDate(new DateTime(2019, 09, 22)),
                new RegistrationCloseDate(new DateTime(2019, 09, 29))
            );
            var requestBody = JsonConvert.SerializeObject(model);
            var result = await client.PostAsync("/v1/events", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // get a status code 200
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            // view that it is the last thing in the EventManagementDto
            var getResult = await client.GetAsync("/v1/events?title=carlie");
            var body = await getResult.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<EventManagementDto>>(body);

            Assert.Equal("Carlie's awesome party", response[1].Title);
        }

        [Fact]
        public async Task GetReturnsAllEventManagementModels()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("/v1/events?title=carlie");
            var body = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<EventManagementDto>>(body);

            Assert.Equal("Carlie's Amazing Event", response[0].Title);
            Assert.Equal(1, response.Count);
        }
    }
}