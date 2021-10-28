using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Interface
{
    public interface IIdentityService
    {
        Task<M.IdentityDto> Authenticate(string username, string password, string userTypeId);
        Task<IEnumerable<M.User>> GetAllPatients(string token);
        Task<int> SaveUser(M.User user, string token);
    }
}
