using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Specialist.API.Core.Interface
{
    public interface ISpecialistService
    {
        Task<M.Specialist> GetSpecialistById(string specId);
        Task<M.PaginatedResults<M.Specialist>> GetSpecialistBySearch(string nric, string specName, int page, int pageSize);
        Task<int> SaveSpecialist(M.Specialist spec);
        Task<int> DeleteSpecialistById(string specId);
    }
}
