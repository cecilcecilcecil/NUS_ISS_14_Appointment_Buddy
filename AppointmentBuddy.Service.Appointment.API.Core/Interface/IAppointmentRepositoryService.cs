using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Appointment.API.Core.Interface
{
    public interface IAppointmentRepositoryService
    {
        Task<M.Appointment> GetAppointmentByAppointmentId(string apptId);

        Task<List<string>> GetFilteredPatientIdsByDate(M.FilteredAppointment mf);
        Task<IEnumerable<M.Appointment>> GetAllAppointments();
        Task<IEnumerable<M.Appointment>> GetAvailableAppointments();
        Task<IEnumerable<M.Appointment>> GetAllMyAppointments(string myId);
        Task<int> SaveAppointment(M.Appointment appt);
        Task<int> UpdateAppointment(M.Appointment appt);
    }
}
