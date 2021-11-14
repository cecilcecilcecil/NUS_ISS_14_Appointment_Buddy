using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Core.Common.Http;
using AppointmentBuddy.Core.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUS_ISS_14_Appointment_Buddy.Helper;
using NUS_ISS_14_Appointment_Buddy.Interface;
using System;
using System.Collections.Generic;
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

            var _api1 = Environment.GetEnvironmentVariable("APPTBUDDY_ROOM_EXTERNAL_DNS_OR_IP");
            if (!string.IsNullOrEmpty(_api1))
            {
                _serviceUrls.RoomAPI = _api1;
            }

            UrlConfig.Room.BaseURI = _serviceUrls.RoomAPI;
            UrlConfig.Room.APIVersion = _serviceUrls.RoomAPIVersion;
        }

        public async Task<M.Room>  GetRoomByRoomId(string roomId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Room.RoomAPI(_serviceUrls.RoomAPI_GetRoomByRoomId, roomId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.Room>(responseString) : null;
   
        }

        public async Task<IEnumerable<M.Room>> GetRoomByServiceId(string serviceId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiURL = UrlConfig.Room.RoomByServiceAPI(_serviceUrls.RoomAPI_GetRoomByServiceId, serviceId);

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<IEnumerable<M.Room>>(responseString) : null;

        }

        public async Task<M.PaginatedResults<M.Room>> GetAllRooms(string token, string desc, int pageIndex, int pageSize)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var parameter = "pageIndex=" + pageIndex + "&pageSize=" + pageSize;

            var apiURL = UrlConfig.Room.AllRoomAPI(_serviceUrls.RoomAPI_GetAllRooms, parameter);

            if (!String.IsNullOrEmpty(desc))
            {
                apiURL = apiURL + "&desc=" + desc;
            }

            var responseString = await _httpClient.GetStringAsync(apiURL);

            return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<M.PaginatedResults<M.Room>>(responseString) : null;
        }

        public async Task<int> SaveRoom(M.Room room, string token)
        {
            int status = Constants.ErrorCodes.Failure;

            var requestContent = new StringContent(JsonConvert.SerializeObject(room), System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Validator.CleanInput(token));

            var apiURL = UrlConfig.Room.SaveRoomAPI(_serviceUrls.RoomAPI_SaveRoom);

            var response = await _httpClient.PostAsync(apiURL, requestContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                status = int.Parse(response.Content.ReadAsStringAsync().Result);
            }

            return status;
        }
    }
}
