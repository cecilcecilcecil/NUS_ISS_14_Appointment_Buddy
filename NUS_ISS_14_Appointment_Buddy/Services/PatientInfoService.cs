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
    public class PatientInfoService : IPatientInfoService
    {
        private readonly IHttpClient _httpClient;
        private readonly ServiceUrls _serviceUrls;
        private readonly CF.AppSettings _appSettings;
        private readonly ILogger<PatientInfoService> _logger;

        public async Task<M.PatientInfo> GetPatientInfoById(string patId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.PatientInfo.PatientInfoAPI(_serviceUrls.PatientInfoAPI_GetPatientInfoById, patId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PatientInfo>(responseString) : null;
        }

        public async Task<M.PatientInfo> UpdatePatientInfo(M.PatientInfo patInfo, string token)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var apiURL = UrlConfig.PatientInfo.PatientInfoAPI(_serviceUrls.PatientInfoAPI_GetPatientInfoById, patId);

            //var responseString = await _httpClient.GetStringAsync(apiURL);

            //return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PatientInfo>(responseString) : null;
            return null;
        }
        public async Task<M.PatientInfo> DeletePatientInfoById(string patId, string token)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var apiURL = UrlConfig.PatientInfo.PatientInfoAPI(_serviceUrls.PatientInfoAPI_GetPatientInfoById, patId);

            //var responseString = await _httpClient.GetStringAsync(apiURL);

            //return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PatientInfo>(responseString) : null;
            return null;
        }


        public async Task<M.PaginatedResults<M.PatientInfo>> GetPatientInfoBySearch(string token, string dateFrom, string dateTo, int pageIndex, int pageSize)
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

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PaginatedResults<M.PatientInfo>>(responseString) : null;
        }

    }
}
