using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Appointment.API.Core.Interface
{
    public interface IAppointmentRepositoryService
    {
        Task<M.Appointment> GetAppointmentByAppointmentId(string apptId);
        Task<IEnumerable<M.Appointment>> GetAllAppointments();
    }
}
