using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Appointment.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Appointment.API.Infrastructure
{
    public class AppointmentRepositoryService : IAppointmentRepositoryService
    {
        private readonly AppointmentBuddyDBContext _context;
        private readonly ILogger<AppointmentRepositoryService> _logger;

        public AppointmentRepositoryService(AppointmentBuddyDBContext context, ILogger<AppointmentRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<M.Appointment> GetAppointmentByAppointmentId(string apptId)
        {
            M.Appointment dataItem;
            dataItem = await _context.Appointment.AsNoTracking().FirstOrDefaultAsync(s => s.AppointmentId == apptId && !s.IsDeleted);
            return dataItem;
        }

        public async Task<List<string>> GetFilteredPatientIdsByDate(M.FilteredAppointment mf)
        {
            var appts = await _context.Appointment.Where(x => x.AppointmentDate == mf.AppointmentDate && x.AppointmentId != mf.AppointmentId && !x.IsDeleted).ToListAsync();

            var ts = TimeSpan.Parse(mf.AppointmentTime);
            var tsMax = ts.Add(new TimeSpan(0, 0, 1800));
            var tsMin = ts.Add(new TimeSpan(0, 0, -1800));

            var uids = appts.Where(x => TimeSpan.Parse(x.AppointmentTime) < tsMax && TimeSpan.Parse(x.AppointmentTime) > tsMin)
                .Select(y => y.UserId).Where(z => !string.IsNullOrEmpty(z))
                .ToList();

            return uids;
        }

        public async Task<IEnumerable<M.Appointment>> GetAllAppointments()
        {
            return await _context.Appointment.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.AppointmentDate)
                .ToListAsync();
        }

        public async Task<int> SaveAppointment(M.Appointment appt)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Add(appt);
            success = await _context.SaveChangesAsync();

            return success;
        }

        public async Task<int> UpdateAppointment(M.Appointment appt)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Update(appt);
            success = await _context.SaveChangesAsync();

            return success;
        }
    }
}
