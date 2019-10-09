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
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Whiteboard.Registration.Web.Services;
using System.Linq;
using Whiteboard.Registration.Domain.Tests.Builders;

namespace Whiteboard.Registration.Web.Tests
{

    public class EventManagementControllerTest
    {

        [Fact]
        public async Task PostAddsANewEventManagementModelWithANewId()
        {
            var repo = new InMemoryEventManagementRepo();
            await repo.Update(
                EventManagementModelBuilder.Build().With(
                    id: new Guid("8b014e41-d74f-4031-a3d7-ae264144b989"),
                    eventTitle: new EventTitle("Brad's Amazing Event")
                ));

            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services => {
                        services.AddSingleton<IEventManagementRepo>(repo);
                    }));
            var client = factory.CreateClient();

            var model = EventManagementModelBuilder.Build().With(
                    id: new Guid("8b014e41-d74f-4031-a3d7-ae264144b989"),
                    eventTitle: new EventTitle("Carlie's awesome party")
                );

            var requestBody = JsonConvert.SerializeObject(model);
            var result = await client.PostAsync("/v1/events", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // get a status code 200
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            // view that it is the last thing in the EventManagementDto
            var getResult = await client.GetAsync("/v1/events");
            var body = await getResult.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<EventManagementDto>>(body);

            Assert.Equal(2, response.Count);
            Assert.Equal(2, response.Select(x => x.Id).Distinct().Count());
            Assert.Equal("Carlie's awesome party", response[1].Title);
        }

        [Fact]
        public async Task GetReturnsAllEventManagementModels()
        {
            var repo = new InMemoryEventManagementRepo();

            await repo.Update(
                new EventManagementModel(
                    new Guid("bcc3b1c4-e74c-4423-8c05-da1c2c8c5510"),
                    new EventTitle("Carlie's Amazing Event"),
                    new EventDescription("a wonderful event"),
                    new EventLocation("Stacktrace Headquarters"),
                    new EventStartDate(new DateTime(2019, 09,30)),
                    new EventEndDate(new DateTime(2019, 10, 20)),
                    new RegistrationOpenDate(new DateTime(2019, 09, 22)),
                    new RegistrationCloseDate(new DateTime(2019, 09, 29)))
                );

            await repo.Update(
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


            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services => {
                        services.AddSingleton<IEventManagementRepo>(repo);
                    }));
            var client = factory.CreateClient();
            var result = await client.GetAsync("/v1/events");
            var body = await result.Content.ReadAsStringAsync();


            var response = JsonConvert.DeserializeObject<List<EventManagementDto>>(body);



            Assert.Equal("Carlie's Amazing Event", response[0].Title);
             Assert.Equal(2, response.Count);
        }

                [Fact]
        public async Task GetReturnsEventManagementModelsWithParameters()
        {
            var repo = new InMemoryEventManagementRepo();

            await repo.Update(
                new EventManagementModel(
                    new Guid("bcc3b1c4-e74c-4423-8c05-da1c2c8c5510"),
                    new EventTitle("Carlie's Amazing Event"),
                    new EventDescription("a wonderful event"),
                    new EventLocation("Stacktrace Headquarters"),
                    new EventStartDate(new DateTime(2019, 09,30)),
                    new EventEndDate(new DateTime(2019, 10, 20)),
                    new RegistrationOpenDate(new DateTime(2019, 09, 22)),
                    new RegistrationCloseDate(new DateTime(2019, 09, 29)))
                );

            await repo.Update(
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


            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services => {
                        services.AddSingleton<IEventManagementRepo>(repo);
                    }));
            var client = factory.CreateClient();
            var result = await client.GetAsync("/v1/events?title=carlie");
            var body = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<EventManagementDto>>(body);

            Assert.Equal("Carlie's Amazing Event", response[0].Title);
            Assert.Equal(1, response.Count);
        }



        [Fact]
        public async Task GetForIdReturnsEventManagementModelWithId()
        {
            var repo = new InMemoryEventManagementRepo();

            await repo.Update(
                new EventManagementModel(
                    new Guid("bcc3b1c4-e74c-4423-8c05-da1c2c8c5510"),
                    new EventTitle("Carlie's Amazing Event"),
                    new EventDescription("a wonderful event"),
                    new EventLocation("Stacktrace Headquarters"),
                    new EventStartDate(new DateTime(2019, 09,30)),
                    new EventEndDate(new DateTime(2019, 10, 20)),
                    new RegistrationOpenDate(new DateTime(2019, 09, 22)),
                    new RegistrationCloseDate(new DateTime(2019, 09, 29)))
                );

            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services => {
                        services.AddSingleton<IEventManagementRepo>(repo);
                    }));


            var client = factory.CreateClient();

            //  This isn't working because it generates a random guid when updated.
            var result = await client.GetAsync("/v1/events/bcc3b1c4-e74c-4423-8c05-da1c2c8c5510");
            var body = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<EventManagementModel>(body);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            Assert.Equal("Carlie's Amazing Event", response.Title.Value);
        }


        [Fact]
        public async Task GetForIdThatDoesntExistReturns404()
        {
            var repo = new InMemoryEventManagementRepo();

            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services => {
                        services.AddSingleton<IEventManagementRepo>(repo);
                    }));
            var client = factory.CreateClient();
            var result = await client.GetAsync("/v1/events/bcc3b1c4-e74c-4423-8c05-da1c2c8c5510");
            var body = await result.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}