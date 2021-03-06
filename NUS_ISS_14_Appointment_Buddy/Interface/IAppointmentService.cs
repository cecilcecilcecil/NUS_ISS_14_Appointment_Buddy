using System.Collections.Generic;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Interface
{
    public interface IAppointmentService
    {
        Task<M.Appointment> GetAppointmentByAppointmentId(string apptId, string token);
        Task<IEnumerable<M.Appointment>> GetAvailableAppointments(string token);
        Task<IEnumerable<M.Appointment>> GetAllAppointmentsByDateRange(string token, string dateFrom, string dateTo);
        Task<M.PaginatedResults<M.Appointment>> GetAllAppointments(string token, string dateFrom, string dateTo, int page, int pageSize);
        Task<M.PaginatedResults<M.Appointment>> GetAllMyAppointments(string token, string dateFrom, string dateTo, string userId, int page, int pageSize);
        Task<List<string>> GetFilteredAppointmentsByPatientIds(M.FilteredAppointment mf, string token);
        Task<int> SaveAppointment(M.Appointment appt, string token);
    }
}
