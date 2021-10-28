using AppointmentBuddy.Service.Services.API.Core.Interface;
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

namespace AppointmentBuddy.Service.Services.API.Infrastructure
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesRepositoryService _repository;
        private readonly ILogger<ServicesService> _logger;

        public ServicesService(HttpClient serviceClient, IServicesRepositoryService repository, ILogger<ServicesService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<M.Services> GetServiceByServicesId(string svcId)
        {
            M.Services response;

            response = await _repository.GetServiceByServicesId(svcId);

            return response;
        }

        public async Task<M.PaginatedResults<M.Services>> GetAllServices(string desc, int page, int pageSize)
        {
            M.PaginatedResults<M.Services> response;

            var data = await _repository.GetAllServices();

            data = data.Where(x => x.Description.Contains(desc)).ToList();

            data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            response = new M.PaginatedResults<M.Services>(page, pageSize, data.Count(), data);

            return response;
        }

        public async Task<int> SaveService(M.Services svc)
        {
            int success = Constants.ErrorCodes.Failure;

            var dbSvc = await _repository.GetServiceByServicesId(svc.ServicesId);

            if (dbSvc == null)
            {
                svc.CreatedBy = svc.LastUpdatedBy;
                svc.CreatedById = svc.LastUpdatedById;
                svc.CreatedDate = DateTime.Now;
                svc.LastUpdatedDate = DateTime.Now;
                svc.VersionNo = 1;

                success = await _repository.SaveService(svc);
            }
            else
            {
                svc.LastUpdatedDate = DateTime.Now;
                svc.VersionNo = dbSvc.VersionNo++;

                success = await _repository.UpdateService(svc);
            }

            return success;
        }
    }
}
