using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Appointment.API.Core.Interface
{
    public interface IAppointmentRepositoryService
    {
        Task<M.Appointment> GetAppointmentByAppointmentId(string apptId);
    }
}
