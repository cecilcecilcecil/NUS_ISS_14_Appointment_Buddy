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
    public class SpecialistService : ISpecialistService
    {
        private readonly IHttpClient _httpClient;
        private readonly ServiceUrls _serviceUrls;
        private readonly CF.AppSettings _appSettings;
        private readonly ILogger<SpecialistService> _logger;

        public SpecialistService(HttpClient httpClient, IOptions<CF.AppSettings> appSettings, IOptions<ServiceUrls> config, ILogger<SpecialistService> logger)
        {
            _httpClient = new CustomHttpClient(httpClient, logger);
            _serviceUrls = config.Value;
            _logger = logger;
            _appSettings = appSettings.Value;

            var _api1 = Environment.GetEnvironmentVariable("APPTBUDDY_SPEC_EXTERNAL_DNS_OR_IP");
            if (!string.IsNullOrEmpty(_api1))
            {
                _serviceUrls.SpecialistAPI = _api1;
            }

            UrlConfig.Specialist.BaseURI = _serviceUrls.SpecialistAPI;
            UrlConfig.Specialist.APIVersion = _serviceUrls.SpecialistAPIVersion;
        }

        public async Task<M.Specialist> GetSpecialistById(string specId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Specialist.SpecialistAPI(_serviceUrls.SpecialistAPI_GetSpecialistById, specId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.Specialist>(responseString) : null;
        }

        public async Task<IEnumerable<M.Specialist>> GetSpecialistByServiceId(string serviceId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Specialist.SpecialistByServiceAPI(_serviceUrls.SpecialistAPI_GetSpecialistByServiceId, serviceId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<IEnumerable<M.Specialist>>(responseString) : null;
        }

        public async Task<int> SaveSpecialist(M.Specialist spec, string token)
        {
            int status = Constants.ErrorCodes.Failure;

            var requestContent = new StringContent(JsonConvert.SerializeObject(spec), System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Validator.CleanInput(token));

            var apiURL = UrlConfig.Specialist.SaveSpecialistAPI(_serviceUrls.SpecialistAPI_SaveSpecialist);

            var response = await _httpClient.PostAsync(apiURL, requestContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                status = int.Parse(response.Content.ReadAsStringAsync().Result);
            }

            return status;
        }
        public async Task<int> DeleteSpecialistById(string specId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Specialist.SpecialistAPI(_serviceUrls.SpecialistAPI_DeleteSpecialistById, specId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<int>(responseString) : Constants.ErrorCodes.Failure;
        }

        public async Task<M.PaginatedResults<M.Specialist>> GetSpecialistBySearch(string token, string nric, string specName, int pageIndex, int pageSize)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var parameter = "pageIndex=" + pageIndex + "&pageSize=" + pageSize;

            var apiURL = UrlConfig.Specialist.SearchSpecialistAPI(_serviceUrls.SpecialistAPI_GetSpecialistBySearch, parameter);

            if (!String.IsNullOrEmpty(nric))
            {
                apiURL = apiURL + "&nric=" + nric;
            }

            if (!String.IsNullOrEmpty(specName))
            {
                apiURL = apiURL + "&specName=" + specName;
            }

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PaginatedResults<M.Specialist>>(responseString) : null;
        }
    }
}
