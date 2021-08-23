using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUS_ISS_14_Appointment_Buddy.Helper;
using NUS_ISS_14_Appointment_Buddy.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IHttpClient _httpClient;
        private readonly ServiceUrls _serviceUrls;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HttpClient httpClient, IOptions<ServiceUrls> config, ILogger<AppointmentService> logger)
        {
            _httpClient = new CustomHttpClient(httpClient, logger);
            _serviceUrls = config.Value;
            _logger = logger;

            var _api1 = Environment.GetEnvironmentVariable("APPTBUDDY_APPT_EXTERNAL_DNS_OR_IP");
            if (!string.IsNullOrEmpty(_api1))
            {
                _serviceUrls.AppointmentAPI = _api1;
            }
            
            UrlConfig.Appointment.BaseURI = _serviceUrls.AppointmentAPI;
            UrlConfig.Appointment.APIVersion = _serviceUrls.AppointmentAPIVersion;
        }

        public async Task<M.Appointment> GetAppointmentByAppointmentId(string apptId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Appointment.AppointmentAPI(_serviceUrls.AppointmentAPI_GetAppointmentByAppointmentId, apptId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.Appointment>(responseString) : null;
        }
    }
}
