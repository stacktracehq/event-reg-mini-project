using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Npgsql;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Domain.Tiny;
using Whiteboard.Registration.Web.Controllers;

namespace Whiteboard.Registration.Web.Services
{
    public class PostgresEventManagementRepo : IEventManagementRepo
    {
        private string _connectionString;

        public PostgresEventManagementRepo(IConfiguration configuration)

        {
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        }

        private IDbConnection GetConnection()
        {
                return new NpgsqlConnection(_connectionString);
        }

        public Task Delete(Guid id)
        {
            using (IDbConnection dbConnection = GetConnection())
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync(
                        "DELETE FROM event_management WHERE id = @Id",
                        new { Id = id}
                );
            }
        }

        public async Task<EventManagementModel> Get(Guid id)
        {
            using (IDbConnection dbConnection = GetConnection())
            {
                dbConnection.Open();
                var value = await dbConnection.QueryFirstOrDefaultAsync(
                    "SELECT * FROM event_management WHERE id = @Id",
                    new { Id = id});

                return new EventManagementModel(
                    id:value.id,
                    title:new EventTitle(value.title),
                    description:new EventDescription(value.description),
                    eventLocation:new EventLocation(value.event_location),
                    eventStartDate: new EventStartDate(value.event_start_date),
                    eventEndDate: new EventEndDate(value.event_end_date),
                    registrationOpenDate:new RegistrationOpenDate(value.registration_open_date),
                    registrationCloseDate:new RegistrationCloseDate(value.registration_close_date));
            }
        }

        // GET /v1/events/
        public Task<IEnumerable<EventManagementDto>> GetAll(string title)
        {
            using (IDbConnection dbConnection = GetConnection())
            {
                dbConnection.Open();
                var events = dbConnection.Query(
                        "SELECT * FROM event_management WHERE @Title IS NULL OR LOWER(title) LIKE LOWER('%' || @Title || '%')",
                        new {Title = title}
                );
                var listOfEvents = new List<EventManagementModel>();
                foreach (var @event in events)
                {
                    listOfEvents.Add(new EventManagementModel(
                    id:@event.id,
                    title:new EventTitle(@event.title),
                    description:new EventDescription(@event.description),
                    eventLocation:new EventLocation(@event.event_location),
                    eventStartDate: new EventStartDate(@event.event_start_date),
                    eventEndDate: new EventEndDate(@event.event_end_date),
                    registrationOpenDate:new RegistrationOpenDate(@event.registration_open_date),
                    registrationCloseDate:new RegistrationCloseDate(@event.registration_close_date)));
                }

                var eventDTO = new List<EventManagementDto>();
                foreach (var @event in listOfEvents)
                {
                    eventDTO.Add(new EventManagementDto{
                        Id = @event.Id,
                        Title = @event.Title.Value
                    });
                }
                return Task.FromResult(eventDTO.AsEnumerable());
            }

        }

        // Put
        public Task Update(EventManagementModel value)
        {
            using (IDbConnection dbConnection = GetConnection())
            {
                dbConnection.Open();
                return Task.FromResult(
                    dbConnection.Query(
                        "UPDATE event_management SET title = @Title, description = @Description, event_location = @EventLocation, event_start_date = @EventStartDate, event_end_date = @EventEndDate, registration_open_date = @RegistrationOpenDate, registration_close_date = @RegistrationCloseDate WHERE id = @Id",
                        new {Id=value.Id, Title =value.Title.Value, Description = value.Description.Value, EventLocation = value.EventLocation.Value, EventStartDate = value.EventStartDate.Value, EventEndDate = value.EventEndDate.Value, RegistrationOpenDate = value.RegistrationOpenDate.Value, RegistrationCloseDate = value.RegistrationCloseDate.Value}
                    ));
            }

        }

        // Post
        public Task Add(EventManagementModel value)
        {
            using (IDbConnection dbConnection = GetConnection())
            {
                dbConnection.Open();
                return Task.FromResult(
                    dbConnection.Execute(
                        "INSERT INTO event_management (id, title, description, event_location, event_start_date, event_end_date, registration_open_date, registration_close_date) VALUES(@Id, @Title, @Description, @EventLocation, @EventStartDate, @EventEndDate, @RegistrationOpenDate, @RegistrationCloseDate)",
                        new {Id=value.Id, Title =value.Title.Value, Description = value.Description.Value, EventLocation = value.EventLocation.Value, EventStartDate = value.EventStartDate.Value, EventEndDate = value.EventEndDate.Value, RegistrationOpenDate = value.RegistrationOpenDate.Value, RegistrationCloseDate = value.RegistrationCloseDate.Value}
                ));
            }
        }
    }
}
