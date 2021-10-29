using AppointmentBuddy.Service.Specialist.API.Core.Interface;
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

namespace AppointmentBuddy.Service.Specialist.API.Infrastructure
{
    public class SpecialistService : ISpecialistService
    {
        private readonly ISpecialistRepositoryService _repository;
        private readonly ILogger<SpecialistService> _logger;

        public SpecialistService(HttpClient serviceClient, ISpecialistRepositoryService repository, ILogger<SpecialistService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<M.Specialist> GetSpecialistById(string specId)
        {
            M.Specialist response;

            response = await _repository.GetSpecialistById(specId);

            return response;
        }

        public async Task<M.PaginatedResults<M.Specialist>> GetSpecialistBySearch(string nric, string specName, int page, int pageSize)
        {
            M.PaginatedResults<M.Specialist> response;

            var data = await _repository.GetSpecialistBySearch(nric, specName);

            data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            response = new M.PaginatedResults<M.Specialist>(page, pageSize, data.Count(), data);

            return response;
        }


        public async Task<int> SaveSpecialist(M.Specialist specInfo)
        {
            int success = Constants.ErrorCodes.Failure;

            var dbSpec = await _repository.GetSpecialistById(specInfo.SpecialistId);

            if (dbSpec == null)
            {
                specInfo.CreatedBy = specInfo.LastUpdatedBy;
                specInfo.CreatedById = specInfo.LastUpdatedById;
                specInfo.CreatedDate = DateTime.Now;
                specInfo.LastUpdatedDate = DateTime.Now;
                specInfo.VersionNo = 1;

                success = await _repository.SaveSpecialist(specInfo);
            }
            else
            {
                specInfo.LastUpdatedDate = DateTime.Now;
                specInfo.VersionNo = dbSpec.VersionNo++;

                success = await _repository.UpdateSpecialist(specInfo);
            }

            return success;
        }

        public async Task<int> DeleteSpecialistById(string patId)
        {
            int success = Constants.ErrorCodes.Failure;

            success = await _repository.DeleteSpecialistById(patId);

            return success;
        }
    }
}
