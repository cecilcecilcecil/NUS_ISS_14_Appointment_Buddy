using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Interface
{
    public interface ISpecialistService
    {
        Task<M.Specialist> GetSpecialistById(string specId, string token);
        Task<IEnumerable<M.Specialist>> GetSpecialistByServiceId(string serviceId, string token);
        Task<M.PaginatedResults<M.Specialist>> GetSpecialistBySearch(string token, string nric, string specName, int page, int pageSize);
        Task<int> SaveSpecialist(M.Specialist spec, string token);
        Task<int> DeleteSpecialistById(string specId, string token);
    }
}
