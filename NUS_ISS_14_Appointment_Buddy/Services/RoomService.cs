using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Http;
using AppointmentBuddy.Core.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUS_ISS_14_Appointment_Buddy.Helper;
using NUS_ISS_14_Appointment_Buddy.Interface;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Services
{
    public class RoomService : IRoomService
    {
        private readonly IHttpClient _httpClient;
        private readonly ServiceUrls _serviceUrls;
        private readonly ILogger<RoomService> _logger;

        public RoomService(HttpClient httpClient, IOptions<ServiceUrls> config, ILogger<RoomService> logger)
        {
            _httpClient = new CustomHttpClient(httpClient, logger);
            _serviceUrls = config.Value;
            _logger = logger;

            var _api1 = Environment.GetEnvironmentVariable("APPTBUDDY_APPT_EXTERNAL_DNS_OR_IP");
            if (!string.IsNullOrEmpty(_api1))
            {
                _serviceUrls.RoomAPI = _api1;
            }

            UrlConfig.Appointment.BaseURI = _serviceUrls.RoomAPI;
            UrlConfig.Appointment.APIVersion = _serviceUrls.RoomAPIVersion;
        }

        public async Task<M.Room>  GetRoomByRoomId(string roomId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Room.RoomAPI(_serviceUrls.RoomAPI_GetRoomByRoomId, roomId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.Room>(responseString) : null;
   
        }
    }
}
