using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.PatientInfo.API.Core.Interface
{
    public interface IPatientInfoRepositoryService
    {
        Task<M.PatientInfo> GetPatientInfoById(string patId);
        Task<IEnumerable<M.PatientInfo>> GetPatientInfoBySearch(string nric, string patName);
        Task<int> SavePatientInfo(M.PatientInfo patInfo);
        Task<int> UpdatePatientInfo(M.PatientInfo patInfo);
        Task<int> DeletePatientInfoById(string patId);
        Task<int> DeactivatePatientInfofoById(string patId);
    }
}
