using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Appointment.API.Core.Interface
{
    public interface IAppointmentService
    {
        Task<M.Appointment> GetAppointmentByAppointmentId(string apptId);
        Task<M.PaginatedResults<M.Appointment>> GetAllAppointments(string dateFrom, string dateTo, int page, int pageSize);
    }
}
