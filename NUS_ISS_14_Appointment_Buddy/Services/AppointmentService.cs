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
using CF = NUS_ISS_14_Appointment_Buddy.WEB.Config;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IHttpClient _httpClient;
        private readonly ServiceUrls _serviceUrls;
        private readonly CF.AppSettings _appSettings;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HttpClient httpClient, IOptions<CF.AppSettings> appSettings, IOptions<ServiceUrls> config, ILogger<AppointmentService> logger)
        {
            _httpClient = new CustomHttpClient(httpClient, logger);
            _serviceUrls = config.Value;
            _logger = logger;
            _appSettings = appSettings.Value;

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

        public async Task<M.PaginatedResults<M.Appointment>> GetAllAppointments(string token, string dateFrom, string dateTo, int pageIndex, int pageSize)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var parameter = "pageIndex=" + pageIndex + "&pageSize=" + pageSize;

            var apiURL = UrlConfig.Appointment.AllAppointmentAPI(_serviceUrls.AppointmentAPI_GetAllAppointments, parameter);

            if (!String.IsNullOrEmpty(dateFrom))
            {
                apiURL = apiURL + "&dateFrom=" + dateFrom.Replace("/", "");
            }

            if (!String.IsNullOrEmpty(dateTo))
            {
                apiURL = apiURL + "&dateTo=" + dateTo.Replace("/", "");
            }

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PaginatedResults<M.Appointment>>(responseString) : null;
        }
    }
}
