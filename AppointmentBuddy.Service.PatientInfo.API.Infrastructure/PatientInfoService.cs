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

namespace AppointmentBuddy.Service.PatientInfo.API.Infrastructure
{
    public class PatientInfoService
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


        public async Task<M.PatientInfo> UpdatePatientInfo(M.PatientInfo patInfo)
        {
            M.PatientInfo response;

            response = await _repository.UpdatePatientInfo(patInfo);

            return response;
        }

        public async Task<M.PatientInfo> DeletePatientInfoById(string patId)
        {
            M.PatientInfo response;

            response = await _repository.DeletePatientInfoById(patId);

            return response;
        }
    }
}
