using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.PatientInfo.API.Core.Interface
{
    public interface IPatientInfoService
    {
        Task<M.PatientInfo> GetPatientInfoById(string patId);
        Task<M.PatientInfo> GetPatientInfoByUserId(string userId);
        Task<M.PaginatedResults<M.PatientInfo>> GetPatientInfoBySearch(string nric, string patName, int page, int pageSize);
        Task<int> SavePatientInfo(M.PatientInfo patInfo);
        Task<int> DeletePatientInfoById(string patId);
        Task<int> DeactivatePatientInfofoById(string patId);
    }
}
