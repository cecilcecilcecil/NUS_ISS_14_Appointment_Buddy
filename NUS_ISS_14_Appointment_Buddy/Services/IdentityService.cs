using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Core.Common.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUS_ISS_14_Appointment_Buddy.Helper;
using NUS_ISS_14_Appointment_Buddy.Interface;
using System;
using CF = NUS_ISS_14_Appointment_Buddy.WEB.Config;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpClient _httpClient;
        private readonly CF.AppSettings _appSettings;
        private readonly ServiceUrls _serviceUrls;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(HttpClient gatewayClient, IOptions<ServiceUrls> config, IOptions<CF.AppSettings> appSettings, ILogger<IdentityService> logger)
        {
            _httpClient = new CustomHttpClient(gatewayClient, logger);
            _serviceUrls = config.Value;
            _logger = logger;
            _appSettings = appSettings.Value;

            //--Use values defined in the ENVIRONMENT variables when available
            var _api = Environment.GetEnvironmentVariable("APPTBUDDY_EXTERNAL_DNS_OR_IP");
            if (!string.IsNullOrEmpty(_api))
            {
                _serviceUrls.IdentityAPI = _api;
            }
            UrlConfig.Identity.BaseURI = _serviceUrls.IdentityAPI;
            UrlConfig.Identity.APIVersion = _serviceUrls.IdentityAPIVersion;

            var s3KeyStoreBucketName = Environment.GetEnvironmentVariable("APPTBUDDY_S3_KeyStoreBucketName");
            if (!String.IsNullOrEmpty(s3KeyStoreBucketName))
            {
                _appSettings.S3KeyStoreBucketName = s3KeyStoreBucketName;
            }

            var s3KeyStoreFilePath = Environment.GetEnvironmentVariable("APPTBUDDY_S3_KeyStoreFilePath");
            if (!String.IsNullOrEmpty(s3KeyStoreFilePath))
            {
                _appSettings.S3KeyStoreFilePath = s3KeyStoreFilePath;
            }

            var publicKeyName = Environment.GetEnvironmentVariable("APPTBUDDY_S3_PublicKeyName");
            if (!String.IsNullOrEmpty(publicKeyName))
            {
                _appSettings.PublicKeyName = publicKeyName;
            }

            var privateKeyName = Environment.GetEnvironmentVariable("APPTBUDDY_S3_PrivateKeyName");
            if (!String.IsNullOrEmpty(privateKeyName))
            {
                _appSettings.PrivateKeyName = privateKeyName;
            }
        }

        public async Task<M.IdentityDto> Authenticate(string username, string password, string userTypeId)
        {
            var response = "";

            var token = await JwtTokenHelper.GenerateJwtTokenAsync(_appSettings);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Validator.CleanInput(token));

            var requestContent = new StringContent(JsonConvert.SerializeObject(new M.UserParameter() { Username = username, Password = password, UserTypeId = userTypeId }), System.Text.Encoding.UTF8, "application/json");

            var apiURL = UrlConfig.Identity.AuthenticateAPI(_serviceUrls.IdentityAPI_Authenticate);

            var responseContent = await _httpClient.PostAsync(apiURL, requestContent);

            if (responseContent.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = responseContent.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }

            return String.IsNullOrEmpty(response) ? null : JsonConvert.DeserializeObject<M.IdentityDto>(response);
        }

        public async Task<IEnumerable<M.User>> GetAllUnassignedPatientsByDateTime(string date, string time, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Identity.FilteredPatientsAPI(_serviceUrls.IdentityAPI_FilteredPatientsAPI);

            if (!String.IsNullOrEmpty(date))
            {
                apiURL = apiURL + "&date=" + date.Replace("/", "");
            }

            if (!String.IsNullOrEmpty(time))
            {
                apiURL = apiURL + "&time=" + time;
            }

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<IEnumerable<M.User>>(responseString) : null;
        }
    }
}
