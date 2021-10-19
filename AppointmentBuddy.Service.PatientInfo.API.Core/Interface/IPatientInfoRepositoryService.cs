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
        Task<M.PatientInfo> UpdatePatientInfo(M.PatientInfo patInfo);
        Task<M.PatientInfo> DeletePatientInfoById(string patId);
    }
}
