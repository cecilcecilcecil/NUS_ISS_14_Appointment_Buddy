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
    }
}
