using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Appointment.API.Core.Interface
{
    public interface IAppointmentService
    {
        Task<M.Appointment> GetAppointmentByAppointmentId(string apptId);
        Task<List<string>> GetFilteredPatientIdsByDate(M.FilteredAppointment mf);
        Task<M.PaginatedResults<M.Appointment>> GetAllAppointments(string dateFrom, string dateTo, int page, int pageSize);
        Task<int> SaveAppointment(M.Appointment appt);
    }
}
