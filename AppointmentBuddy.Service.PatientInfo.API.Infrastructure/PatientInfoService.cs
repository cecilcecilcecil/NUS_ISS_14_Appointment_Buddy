using AppointmentBuddy.Service.PatientInfo.API.Core.Interface;
using System;
using System.Collections.Generic;
using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Globalization;
using System.Linq;
using AppointmentBuddy.Core.Common.Helper;

namespace AppointmentBuddy.Service.PatientInfo.API.Infrastructure
{
    public class PatientInfoService : IPatientInfoService
    {
        private readonly IPatientInfoRepositoryService _repository;
        private readonly ILogger<PatientInfoService> _logger;

        public PatientInfoService(HttpClient serviceClient, IPatientInfoRepositoryService repository, ILogger<PatientInfoService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<M.PatientInfo> GetPatientInfoById(string patId)
        {
            M.PatientInfo response;

            response = await _repository.GetPatientInfoById(patId);

            return response;
        }

        public async Task<M.PaginatedResults<M.PatientInfo>> GetPatientInfoBySearch(string nric, string patName, int page, int pageSize)
        {
            M.PaginatedResults<M.PatientInfo> response;

            var data = await _repository.GetPatientInfoBySearch(nric, patName);

            data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            response = new M.PaginatedResults<M.PatientInfo>(page, pageSize, data.Count(), data);

            return response;
        }


        public async Task<int> SavePatientInfo(M.PatientInfo patInfo)
        {
            int success = Constants.ErrorCodes.Failure;

            var dbpat = await _repository.GetPatientInfoById(patInfo.PatientId);
            if (dbpat == null)
            {
                patInfo.CreatedBy = patInfo.LastUpdatedBy;
                patInfo.CreatedById = patInfo.LastUpdatedById;
                patInfo.CreatedDate = DateTime.Now;
                patInfo.LastUpdatedDate = DateTime.Now;

                success = await _repository.SavePatientInfo(patInfo);
            }
            else
            {
                patInfo.LastUpdatedDate = DateTime.Now;

                success = await _repository.UpdatePatientInfo(patInfo);
            }

            return success;
        }

        public async Task<int> DeletePatientInfoById(string patId)
        {
            int success = Constants.ErrorCodes.Failure;

            success = await _repository.DeletePatientInfoById(patId);

            return success;
        }
        public async Task<int> DeactivatePatientInfofoById(string patId)
        {
            int success = Constants.ErrorCodes.Failure;

            success = await _repository.DeactivatePatientInfofoById(patId);

            return success;
        }
    }
}
