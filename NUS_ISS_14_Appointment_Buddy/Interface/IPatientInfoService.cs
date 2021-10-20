using System.Collections.Generic;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;
namespace NUS_ISS_14_Appointment_Buddy.Interface
{
    public interface IPatientInfoService
    {
        Task<M.PatientInfo> GetPatientInfoById(string patId, string token);
        Task<M.PaginatedResults<M.PatientInfo>> GetPatientInfoBySearch(string token, string nric, string patname, int page, int pageSize);
        Task<int> SavePatientInfo(M.PatientInfo patInfo, string token);
        Task<int> DeletePatientInfoById(string patId, string token);
        Task<int> DeactivatePatientInfoById(string patId, string token);
    }
}
