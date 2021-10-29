using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Specialist.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Specialist.API.Infrastructure
{
    public class SpecialistRepositoryService : ISpecialistRepositoryService
    {
        private readonly AppointmentBuddyDBContext _context;
        private readonly ILogger<SpecialistRepositoryService> _logger;

        public SpecialistRepositoryService(AppointmentBuddyDBContext context, ILogger<SpecialistRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<M.Specialist> GetSpecialistById(string specId)
        {
            M.Specialist dataItem;
            dataItem = await _context.Specialist.AsNoTracking().FirstOrDefaultAsync(s => s.SpecialistId == specId);
            return dataItem;
        }

        public async Task<IEnumerable<M.Specialist>> GetSpecialistByServiceId(string serviceId)
        {
            IEnumerable<M.Specialist> dataItem;
            dataItem = await _context.Specialist.AsNoTracking().Where(s => s.ServicesId == serviceId).ToListAsync();
            return dataItem;
        }

        public async Task<IEnumerable<M.Specialist>> GetSpecialistBySearch(string nric, string specName)
        {
            if (string.IsNullOrEmpty(nric))
            {
                return await _context.Specialist.Where(x => x.Name.Contains(specName) && !x.IsDeleted).OrderBy(x => x.Name).ToListAsync();
            }

            return await _context.Specialist.Where(x => x.Nric == nric && x.Name.Contains(specName) && !x.IsDeleted).OrderBy(x => x.Name).ToListAsync();
        }


        public async Task<int> DeleteSpecialistById(string specId)
        {
            int success = Constants.ErrorCodes.Failure;
            M.Specialist dataItem = await _context.Specialist.FirstOrDefaultAsync(s => s.SpecialistId == specId);
            dataItem.IsDeleted = true;
            _context.SaveChanges();

            success = Constants.ErrorCodes.Success;
            return success;
        }

        public async Task<int> SaveSpecialist(M.Specialist patInfo)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Add(patInfo);
            success = await _context.SaveChangesAsync();

            return success;
        }

        public async Task<int> UpdateSpecialist(M.Specialist patInfo)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Update(patInfo);
            success = await _context.SaveChangesAsync();

            return success;
        }
    }
}
