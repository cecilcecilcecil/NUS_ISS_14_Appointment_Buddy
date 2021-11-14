using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Specialist.API.Core.Interface
{
    public interface ISpecialistRepositoryService
    {
        Task<M.Specialist> GetSpecialistById(string specId);
        Task<IEnumerable<M.Specialist>> GetSpecialistByServiceId(string serviceId);
        Task<IEnumerable<M.Specialist>> GetSpecialistBySearch(string nric, string specName);
        Task<int> SaveSpecialist(M.Specialist specInfo);
        Task<int> UpdateSpecialist(M.Specialist specInfo);
        Task<int> DeleteSpecialistById(string specId);
    }
}
