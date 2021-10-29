using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Services.API.Core.Interface
{
    public interface IServicesService
    {
        Task<M.Services> GetServiceByServicesId(string svcId);
        Task<IEnumerable<M.Services>> GetAllNonPageServices();
        Task<M.PaginatedResults<M.Services>> GetAllServices(string desc, int page, int pageSize);
        Task<int> SaveService(M.Services svc);
    }
}
