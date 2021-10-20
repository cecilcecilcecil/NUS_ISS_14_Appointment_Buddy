using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Identity.API.Core.Interface
{
    public interface IIdentityService
    {
        Task<M.IdentityDto> Authenticate(string username, string password, string userTypeId);
        Task<IEnumerable<M.User>> GetAllPatients();
    }
}
