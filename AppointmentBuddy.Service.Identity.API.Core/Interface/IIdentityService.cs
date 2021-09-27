using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Identity.API.Core.Interface
{
    public interface IIdentityService
    {
        Task<M.IdentityDto> Authenticate(string username, string password, string userTypeId);
    }
}
