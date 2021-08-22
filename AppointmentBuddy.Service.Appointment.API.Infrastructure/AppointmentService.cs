using AppointmentBuddy.Service.Appointment.API.Core.Interface;
using System;
using System.Collections.Generic;
using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace AppointmentBuddy.Service.Appointment.API.Infrastructure
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepositoryService _repository;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HttpClient serviceClient, IAppointmentRepositoryService repository, ILogger<AppointmentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<M.Appointment> GetAppointmentByAppointmentId(string appointmentId)
        {
            M.Appointment response;

            //if (InternetZone)
            //{
            //    SetAuthenticationHeader(_apiClient);

            //    var apiURL = ServiceEndpoint.CodeTables.ConfigurationsAPI(_serviceUrls.CodeTablesAPI_Configurations);
            //    var _api_response = await _apiClient.GetStringAsync(apiURL);

            //    response = !string.IsNullOrEmpty(_api_response) ? JsonConvert.DeserializeObject<IEnumerable<M.Appointment>>(_api_response) : null;
            //}

            response = await _repository.GetAppointmentByAppointmentId(appointmentId);

            return response;
        }
    }
}
