using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Identity.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Identity.API.Infrastructure.Repository
{
    public class IdentityRepositoryService : IIdentityRepositoryService
    {
        private readonly AppointmentBuddyDBContext _context;
        private readonly ILogger<IdentityRepositoryService> _logger;

        public IdentityRepositoryService(AppointmentBuddyDBContext context, ILogger<IdentityRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<M.User> GetUserByUserLogin(string userLogin, string userTypeId = "")
        {
            M.User user;
            if (String.IsNullOrEmpty(userLogin))
            {
                //--TODO: log 
                user = null;
            }
            else
            {
                _logger.LogInformation("Login as: " + userTypeId);
                if (string.IsNullOrEmpty(userTypeId))
                {
                    user = await _context.User.FirstOrDefaultAsync(item => item.UserLogin == userLogin && item.IsDeleted == false);
                }
                else
                {
                    user = await _context.User.FirstOrDefaultAsync(item => item.UserLogin == userLogin && item.UserTypeId == userTypeId && item.IsDeleted == false);
                }
            }

            if (user != null)
            {
                _logger.LogInformation("Final User Type: " + user.UserTypeId + " and user ID: " + user.UserId);
            }

            return user;
        }

        public async Task<M.UserRole> GetUserRoleByUserId(string userId)
        {
            M.UserRole userRole = await _context.User
                                            .Where(e => e.UserId == userId && e.IsDeleted == false)
                                            .Join(_context.UserRole, x => x.UserId, y => y.UserId, (x, y) => y)
                                            .Where(e => e.IsDeleted == false)
                                            .FirstOrDefaultAsync();

            return userRole;
        }

        public async Task<M.Role> GetRoleByUserId(string userId)
        {
            M.Role role = await _context.User
                                            .Where(e => e.UserId == userId && e.IsDeleted == false)
                                            .Join(_context.UserRole, x => x.UserId, y => y.UserId, (x, y) => y)
                                            .Where(e => e.IsDeleted == false)
                                            .Join(_context.Role, x => x.RoleId, y => y.RoleId, (x, y) => y)
                                            .Where(e => e.IsDeleted == false)
                                            .FirstOrDefaultAsync();

            return role;
        }
    }
}
