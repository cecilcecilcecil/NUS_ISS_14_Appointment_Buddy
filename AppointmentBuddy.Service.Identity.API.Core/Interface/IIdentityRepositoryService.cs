using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Identity.API.Core.Interface
{
    public interface IIdentityRepositoryService
    {
        Task<M.User> GetUserByUserLogin(string userLogin, string password, string userTypeId);
        Task<M.UserRole> GetUserRoleByUserId(string userId);
        Task<M.Role> GetRoleByUserId(string userId);
        Task<IEnumerable<M.User>> GetAllPatients();
    }
}
