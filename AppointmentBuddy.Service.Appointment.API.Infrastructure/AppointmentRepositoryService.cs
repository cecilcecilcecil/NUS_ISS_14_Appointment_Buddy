﻿using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Appointment.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            dataItem = await _context.Appointment.FirstOrDefaultAsync(s => s.AppointmentId == apptId);
            return dataItem;
        }

        public async Task<IEnumerable<M.Appointment>> GetAllAppointments()
        {
            return await _context.Appointment.Where(x => !x.IsDeleted && x.UserId != null).ToListAsync();
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
