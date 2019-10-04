using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Web.Controllers;

namespace Whiteboard.Registration.Web.Services
{
    public interface IEventManagementRepo {

        Task<IEnumerable<EventManagementDto>> GetAll(string title);
        Task<EventManagementModel> Get(Guid id);
        Task Update(Guid id, EventManagementModel value);
        Task Update(EventManagementModel value);
        Task Delete(Guid id);
    }

}
