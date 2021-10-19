using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.PatientInfo.API.Core.Interface
{
    public interface IPatientInfoService
    {
        Task<M.PatientInfo> GetPatientInfoById(string patId);
        Task<M.PaginatedResults<M.PatientInfo>> GetPatientInfoBySearch(string nric, string patName, int page, int pageSize);
        Task<M.PatientInfo> UpdatePatientInfo(M.PatientInfo patInfo);
        Task<M.PatientInfo> DeletePatientInfoById(string patId);
    }
}
