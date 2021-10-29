using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Appointment.API.Core.Interface
{
    public interface IAppointmentService
    {
        Task<M.Appointment> GetAppointmentByAppointmentId(string apptId);
        Task<List<string>> GetFilteredPatientIdsByDate(M.FilteredAppointment mf);
        Task<IEnumerable<M.Appointment>> GetAvailableAppointments();
        Task<IEnumerable<M.Appointment>> GetAllAppointmentsByDateRange(string dateFrom, string dateTo);
        Task<M.PaginatedResults<M.Appointment>> GetAllAppointments(string dateFrom, string dateTo, int page, int pageSize);
        Task<M.PaginatedResults<M.Appointment>> GetAllMyAppointments(string dateFrom, string dateTo, string userId, int page, int pageSize);
        Task<int> SaveAppointment(M.Appointment appt);
    }
}
