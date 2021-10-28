using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Services.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Services.API.Infrastructure
{
    public class ServicesRepositoryService : IServicesRepositoryService
    {
        private readonly AppointmentBuddyDBContext _context;
        private readonly ILogger<ServicesRepositoryService> _logger;

        public ServicesRepositoryService(AppointmentBuddyDBContext context, ILogger<ServicesRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<M.Services> GetServiceByServicesId(string svcId)
        {
            M.Services dataItem;
            dataItem = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.ServicesId == svcId && !s.IsDeleted);
            return dataItem;
        }
        public async Task<IEnumerable<M.Services>> GetAllServices()
        {
            return await _context.Services.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<int> SaveService(M.Services svc)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Add(svc);
            success = await _context.SaveChangesAsync();

            return success;
        }

        public async Task<int> UpdateService(M.Services svc)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Update(svc);
            success = await _context.SaveChangesAsync();

            return success;
        }
    }
}
