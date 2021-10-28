using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Interface
{
    public interface IServicesService
    {
        Task<M.Services> GetServiceByServicesId(string svcId, string token);
        Task<M.PaginatedResults<M.Services>> GetAllServices(string token, string desc, int page, int pageSize);
        Task<int> SaveService(M.Services svc, string token);
    }
}
