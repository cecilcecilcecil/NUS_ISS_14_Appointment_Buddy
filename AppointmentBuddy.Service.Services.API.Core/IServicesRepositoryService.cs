using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Services.API.Core.Interface
{
    public interface IServicesRepositoryService
    {
        Task<M.Services> GetServiceByServicesId(string apptId);
        Task<IEnumerable<M.Services>> GetAllServices();
        Task<int> SaveService(M.Services appt);
        Task<int> UpdateService(M.Services appt);
    }
}
