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
    }
}
