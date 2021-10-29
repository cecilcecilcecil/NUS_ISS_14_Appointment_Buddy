using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Helper;
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

        public async Task<List<string>> GetFilteredAppointmentsByPatientIds(M.FilteredAppointment mf, string token)
        {
            List<string> data = new List<string>();

            var requestContent = new StringContent(JsonConvert.SerializeObject(mf), System.Text.Encoding.UTF8, "application/json");

            var apiURL = UrlConfig.Appointment.SearchAppointmentAPI(_serviceUrls.AppointmentAPI_GetFilteredAppointmentsByPatientIds);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Validator.CleanInput(token));

            var response = await _httpClient.PostAsync(apiURL, requestContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                data = !string.IsNullOrEmpty(jsonResult) ? JsonConvert.DeserializeObject<List<string>>(jsonResult) : null;
            }

            return data;
        }

        public async Task<IEnumerable<M.Appointment>> GetAvailableAppointments(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Appointment.GetAvailableAppointmentsAPI(_serviceUrls.AppointmentAPI_GetAvailableAppointments);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<IEnumerable<M.Appointment>>(responseString) : null;
        }

        public async Task<IEnumerable<M.Appointment>> GetAllAppointmentsByDateRange(string token, string dateFrom, string dateTo)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Appointment.GetAllAppointmentsByDateRangeAPI(_serviceUrls.AppointmentAPI_GetAllAppointmentsByDateRange, dateFrom.Replace("/", ""), dateTo.Replace("/", ""));

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<IEnumerable<M.Appointment>>(responseString) : null;
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

        public async Task<M.PaginatedResults<M.Appointment>> GetAllMyAppointments(string token, string dateFrom, string dateTo, string userId, int pageIndex, int pageSize)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var parameter = "pageIndex=" + pageIndex + "&pageSize=" + pageSize;

            var apiURL = UrlConfig.Appointment.AllAppointmentAPI(_serviceUrls.AppointmentAPI_GetAllMyAppointments, parameter);

            if (!String.IsNullOrEmpty(dateFrom))
            {
                apiURL = apiURL + "&dateFrom=" + dateFrom.Replace("/", "");
            }

            if (!String.IsNullOrEmpty(dateTo))
            {
                apiURL = apiURL + "&dateTo=" + dateTo.Replace("/", "");
            }

            if (!String.IsNullOrEmpty(userId))
            {
                apiURL = apiURL + "&userId=" + userId;
            }

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PaginatedResults<M.Appointment>>(responseString) : null;
        }

        public async Task<int> SaveAppointment(M.Appointment appt, string token)
        {
            int status = Constants.ErrorCodes.Failure;

            var requestContent = new StringContent(JsonConvert.SerializeObject(appt), System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Validator.CleanInput(token));

            var apiURL = UrlConfig.Appointment.SaveAppointmentAPI(_serviceUrls.AppointmentAPI_SaveAppointment);

            var response = await _httpClient.PostAsync(apiURL, requestContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                status = int.Parse(response.Content.ReadAsStringAsync().Result);
            }

            return status;
        }
    }
}
