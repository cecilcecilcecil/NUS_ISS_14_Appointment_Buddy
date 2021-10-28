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
    public class ServicesService : IServicesService
    {
        private readonly IHttpClient _httpClient;
        private readonly ServiceUrls _serviceUrls;
        private readonly CF.AppSettings _appSettings;
        private readonly ILogger<ServicesService> _logger;

        public ServicesService(HttpClient httpClient, IOptions<CF.AppSettings> appSettings, IOptions<ServiceUrls> config, ILogger<ServicesService> logger)
        {
            _httpClient = new CustomHttpClient(httpClient, logger);
            _serviceUrls = config.Value;
            _logger = logger;
            _appSettings = appSettings.Value;

            var _api1 = Environment.GetEnvironmentVariable("APPTBUDDY_SVC_EXTERNAL_DNS_OR_IP");
            if (!string.IsNullOrEmpty(_api1))
            {
                _serviceUrls.ServicesAPI = _api1;
            }

            UrlConfig.Services.BaseURI = _serviceUrls.ServicesAPI;
            UrlConfig.Services.APIVersion = _serviceUrls.ServicesAPIVersion;
        }


        public async Task<M.Services> GetServiceByServicesId(string svcId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Services.ServiceAPI(_serviceUrls.ServicesAPI_GetServiceByServicesId, svcId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.Services>(responseString) : null;
        }

        public async Task<M.PaginatedResults<M.Services>> GetAllServices(string token, string desc, int pageIndex, int pageSize)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var parameter = "pageIndex=" + pageIndex + "&pageSize=" + pageSize;

            var apiURL = UrlConfig.Services.AllServiceAPI(_serviceUrls.ServicesAPI_GetAllServices, parameter);

            if (!String.IsNullOrEmpty(desc))
            {
                apiURL = apiURL + "&desc=" + desc;
            }

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PaginatedResults<M.Services>>(responseString) : null;
        }

        public async Task<int> SaveService(M.Services svc, string token)
        {
            int status = Constants.ErrorCodes.Failure;

            var requestContent = new StringContent(JsonConvert.SerializeObject(svc), System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Validator.CleanInput(token));

            var apiURL = UrlConfig.Services.SaveServicesAPI(_serviceUrls.ServicesAPI_SaveService);

            var response = await _httpClient.PostAsync(apiURL, requestContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                status = int.Parse(response.Content.ReadAsStringAsync().Result);
            }

            return status;
        }
    }
}
