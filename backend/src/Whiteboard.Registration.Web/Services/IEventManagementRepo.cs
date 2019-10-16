using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whiteboard.Registration.Domain;
using Whiteboard.Registration.Web.Models;

namespace Whiteboard.Registration.Web.Services
{
    public interface IEventManagementRepo {

        Task<IEnumerable<EventManagementDto>> GetAll(string title);
        Task<EventManagementModel> Get(Guid id);
        Task Update(EventManagementModel value);
        Task Add(EventManagementModel value);
        Task Delete(Guid id);
    }

}
