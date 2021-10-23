using AppointmentBuddy.Service.Room.API.Core.Interface;
using System;
using System.Collections.Generic;
using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace AppointmentBuddy.Service.Room.API.Infrastructure
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepositoryService _repository;
        private readonly ILogger<RoomService> _logger;

        public RoomService(HttpClient serviceClient, IRoomRepositoryService repository, ILogger<RoomService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<M.Room> GetRoomByRoomId(string roomId)
        {
            M.Room response;

            //if (InternetZone)
            //{
            //    SetAuthenticationHeader(_apiClient);

            //    var apiURL = ServiceEndpoint.CodeTables.ConfigurationsAPI(_serviceUrls.CodeTablesAPI_Configurations);
            //    var _api_response = await _apiClient.GetStringAsync(apiURL);

            //    response = !string.IsNullOrEmpty(_api_response) ? JsonConvert.DeserializeObject<IEnumerable<M.Appointment>>(_api_response) : null;
            //}

            response = await _repository.GetRoomByRoomId(roomId);

            return response;
        }
    }
}
