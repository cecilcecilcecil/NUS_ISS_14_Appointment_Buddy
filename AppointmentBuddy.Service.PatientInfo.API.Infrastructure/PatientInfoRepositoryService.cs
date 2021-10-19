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
    public class PatientInfoRepositoryService
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
            dataItem = await _context.PatientInfo.FirstOrDefaultAsync(s => s.PatientId == patId);
            return dataItem;
        }

        public async Task<IEnumerable<M.PatientInfo>> GetPatienInfoBySearch(string nric, string patName)
        {
            if (string.IsNullOrEmpty(nric))
            {
                return await _context.PatientInfo.Where(x => x.PatientName.Contains(patName) && !x.IsDeleted).ToListAsync();
            }

            return await _context.PatientInfo.Where(x => x.NRIC == nric && x.PatientName.Contains(patName) && !x.IsDeleted).ToListAsync();
        }

        public async Task<M.PatientInfo> UpdatePatientInfo(M.PatientInfo patInfo)
        {
            M.PatientInfo dataItem;
            dataItem = await _context.PatientInfo.FirstOrDefaultAsync(s => s.PatientId == patInfo.PatientId);
            dataItem.Title = patInfo.Title;
            dataItem.NRIC = patInfo.NRIC;
            return dataItem;
        }

        public async Task<M.PatientInfo> DeletePatientInfoById(string patId)
        {
            M.PatientInfo dataItem;
            dataItem = await _context.PatientInfo.FirstOrDefaultAsync(s => s.PatientId == patId);
            dataItem.IsDeleted = true;
            _context.SaveChanges();
            return dataItem;
        }
    }
}
