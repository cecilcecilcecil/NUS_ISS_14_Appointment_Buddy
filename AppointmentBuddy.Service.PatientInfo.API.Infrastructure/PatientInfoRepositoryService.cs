using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.PatientInfo.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.PatientInfo.API.Infrastructure
{
    public class PatientInfoRepositoryService : IPatientInfoRepositoryService
    {
        private readonly AppointmentBuddyDBContext _context;
        private readonly ILogger<PatientInfoRepositoryService> _logger;

        public PatientInfoRepositoryService(AppointmentBuddyDBContext context, ILogger<PatientInfoRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<M.PatientInfo> GetPatientInfoById(string patId)
        {
            M.PatientInfo dataItem;
            dataItem = await _context.PatientInfo.AsNoTracking().FirstOrDefaultAsync(s => s.PatientId == patId);
            return dataItem;
        }

        public async Task<M.PatientInfo> GetPatientInfoByUserId(string userId)
        {
            M.PatientInfo dataItem;
            dataItem = await _context.PatientInfo.AsNoTracking().FirstOrDefaultAsync(s => s.UserId == userId);
            return dataItem;
        }

        public async Task<IEnumerable<M.PatientInfo>> GetPatientInfoBySearch(string nric, string patName)
        {
            if (string.IsNullOrEmpty(nric))
            {
                return await _context.PatientInfo.Where(x => x.PatientName.Contains(patName) && !x.IsDeleted).OrderBy(x => x.PatientName).ToListAsync();
            }

            return await _context.PatientInfo.Where(x => x.NRIC == nric && x.PatientName.Contains(patName) && !x.IsDeleted).OrderBy(x => x.PatientName).ToListAsync();
        }


        public async Task<int> DeletePatientInfoById(string patId)
        {
            int success = Constants.ErrorCodes.Failure;
            M.PatientInfo dataItem = await _context.PatientInfo.FirstOrDefaultAsync(s => s.PatientId == patId);
            dataItem.IsDeleted = true;
            _context.SaveChanges();

            success = Constants.ErrorCodes.Success;
            return success;
        }

        public async Task<int> DeactivatePatientInfofoById(string patId)
        {
            int success = Constants.ErrorCodes.Failure;
            M.PatientInfo dataItem = await _context.PatientInfo.FirstOrDefaultAsync(s => s.PatientId == patId);
            dataItem.DeathDate = System.DateTime.Now;
            _context.SaveChanges();
            return success;
        }

        public async Task<int> SavePatientInfo(M.PatientInfo patInfo)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Add(patInfo);
            success = await _context.SaveChangesAsync();

            return success;
        }

        public async Task<int> UpdatePatientInfo(M.PatientInfo patInfo)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Update(patInfo);
            success = await _context.SaveChangesAsync();

            return success;
        }



    }
}
