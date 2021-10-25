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
    public class PatientInfoService : IPatientInfoService
    {
        private readonly IHttpClient _httpClient;
        private readonly ServiceUrls _serviceUrls;
        private readonly CF.AppSettings _appSettings;
        private readonly ILogger<PatientInfoService> _logger;

        public PatientInfoService(HttpClient httpClient, IOptions<CF.AppSettings> appSettings, IOptions<ServiceUrls> config, ILogger<PatientInfoService> logger)
        {
            _httpClient = new CustomHttpClient(httpClient, logger);
            _serviceUrls = config.Value;
            _logger = logger;
            _appSettings = appSettings.Value;

            var _api1 = Environment.GetEnvironmentVariable("APPTBUDDY_PTINFO_EXTERNAL_DNS_OR_IP");
            if (!string.IsNullOrEmpty(_api1))
            {
                _serviceUrls.AppointmentAPI = _api1;
            }

            UrlConfig.PatientInfo.BaseURI = _serviceUrls.PatientInfoAPI;
            UrlConfig.PatientInfo.APIVersion = _serviceUrls.PatientInfoAPIVersion;
        }

        public async Task<M.PatientInfo> GetPatientInfoById(string patId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.PatientInfo.PatientInfoAPI(_serviceUrls.PatientInfoAPI_GetPatientInfoById, patId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PatientInfo>(responseString) : null;
        }

        public async Task<int> SavePatientInfo(M.PatientInfo patInfo, string token)
        {
            int status = Constants.ErrorCodes.Failure;

            var requestContent = new StringContent(JsonConvert.SerializeObject(patInfo), System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Validator.CleanInput(token));

            var apiURL = UrlConfig.PatientInfo.SavePatientInfoAPI(_serviceUrls.PatientInfoAPI_SavePatientInfo);

            var response = await _httpClient.PostAsync(apiURL, requestContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                status = int.Parse(response.Content.ReadAsStringAsync().Result);
            }

            return status;
        }
        public async Task<int> DeletePatientInfoById(string patId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.PatientInfo.PatientInfoAPI(_serviceUrls.PatientInfoAPI_DeletePatientInfoById, patId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<int>(responseString) : Constants.ErrorCodes.Failure;
        }

        public async Task<int> DeactivatePatientInfoById(string patId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.PatientInfo.PatientInfoAPI(_serviceUrls.PatientInfoAPI_DeactivatePatientInfoById, patId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<int>(responseString) : Constants.ErrorCodes.Failure;
        }


        public async Task<M.PaginatedResults<M.PatientInfo>> GetPatientInfoBySearch(string token, string nric, string patName, int pageIndex, int pageSize)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var parameter = "pageIndex=" + pageIndex + "&pageSize=" + pageSize;

            var apiURL = UrlConfig.PatientInfo.SearchPatientInfoAPI(_serviceUrls.PatientInfoAPI_GetPatientInfoBySearch, parameter);

            if (!String.IsNullOrEmpty(nric))
            {
                apiURL = apiURL + "&nric=" + nric;
            }

            if (!String.IsNullOrEmpty(patName))
            {
                apiURL = apiURL + "&patName=" + patName;
            }

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PaginatedResults<M.PatientInfo>>(responseString) : null;
        }

    }
}
