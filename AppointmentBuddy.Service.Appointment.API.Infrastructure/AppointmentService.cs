using AppointmentBuddy.Service.Appointment.API.Core.Interface;
using System;
using System.Collections.Generic;
using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Globalization;
using System.Linq;
using AppointmentBuddy.Core.Common.Helper;

namespace AppointmentBuddy.Service.Appointment.API.Infrastructure
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepositoryService _repository;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HttpClient serviceClient, IAppointmentRepositoryService repository, ILogger<AppointmentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<M.Appointment> GetAppointmentByAppointmentId(string appointmentId)
        {
            M.Appointment response;

            response = await _repository.GetAppointmentByAppointmentId(appointmentId);

            return response;
        }

        public async Task<List<string>> GetFilteredPatientIdsByDate(M.FilteredAppointment mf)
        {
            var appts = await _repository.GetFilteredPatientIdsByDate(mf);

            return appts;
        }

        public async Task<M.PaginatedResults<M.Appointment>> GetAllAppointments(string dateFrom, string dateTo, int page, int pageSize)
        {
            M.PaginatedResults<M.Appointment> response;

            var data = await _repository.GetAllAppointments();

            DateTime dtFrom;
            if (!String.IsNullOrEmpty(dateFrom) && DateTime.TryParseExact(dateFrom, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom))
            {
                data = data.Where(t => t.AppointmentDate > dtFrom);
            }

            DateTime dtTo;
            if (!String.IsNullOrEmpty(dateTo) && DateTime.TryParseExact(dateTo, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo))
            {
                dtTo = dtTo.AddDays(1);
                data = data.Where(t => t.AppointmentDate < dtTo);
            }

            data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            response = new M.PaginatedResults<M.Appointment>(page, pageSize, data.Count(), data);

            return response;
        }

        public async Task<M.PaginatedResults<M.Appointment>> GetAllMyAppointments(string dateFrom, string dateTo, string myId, int page, int pageSize)
        {
            M.PaginatedResults<M.Appointment> response;

            var data = await _repository.GetAllMyAppointments(myId);

            DateTime dtFrom;
            if (!String.IsNullOrEmpty(dateFrom) && DateTime.TryParseExact(dateFrom, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom))
            {
                data = data.Where(t => t.AppointmentDate > dtFrom);
            }

            DateTime dtTo;
            if (!String.IsNullOrEmpty(dateTo) && DateTime.TryParseExact(dateTo, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo))
            {
                dtTo = dtTo.AddDays(1);
                data = data.Where(t => t.AppointmentDate < dtTo);
            }

            data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            response = new M.PaginatedResults<M.Appointment>(page, pageSize, data.Count(), data);

            return response;
        }

        public async Task<IEnumerable<M.Appointment>> GetAvailableAppointments()
        {
            var response = await _repository.GetAvailableAppointments();

            response = response.Where(x =>
                x.AppointmentDate.GetValueOrDefault().Date == DateTime.Now.Date && TimeSpan.Parse(x.AppointmentTime) >= DateTime.Now.TimeOfDay
                || x.AppointmentDate.GetValueOrDefault().Date >= DateTime.Now.Date);

            return response;
        }

        public async Task<IEnumerable<M.Appointment>> GetAllAppointmentsByDateRange(string dateFrom, string dateTo)
        {
            var data = await _repository.GetAllAppointments();

            DateTime dtFrom;
            if (!String.IsNullOrEmpty(dateFrom) && DateTime.TryParseExact(dateFrom, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom))
            {
                data = data.Where(t => t.AppointmentDate > dtFrom);
            }

            DateTime dtTo;
            if (!String.IsNullOrEmpty(dateTo) && DateTime.TryParseExact(dateTo, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo))
            {
                dtTo = dtTo.AddDays(1);
                data = data.Where(t => t.AppointmentDate < dtTo);
            }

            return data;
        }

        public async Task<int> SaveAppointment(M.Appointment appt)
        {
            int success = Constants.ErrorCodes.Failure;

            var dbAppt = await _repository.GetAppointmentByAppointmentId(appt.AppointmentId);

            if (dbAppt == null)
            {
                appt.CreatedBy = appt.LastUpdatedBy;
                appt.CreatedById = appt.LastUpdatedById;
                appt.CreatedDate = DateTime.Now;
                appt.LastUpdatedDate = DateTime.Now;
                appt.VersionNo = 1;

                success = await _repository.SaveAppointment(appt);
            }
            else
            {
                appt.LastUpdatedDate = DateTime.Now;
                appt.VersionNo = dbAppt.VersionNo++;

                success = await _repository.UpdateAppointment(appt);
            }

            return success;
        }
    }
}
