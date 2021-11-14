using AppointmentBuddy.Service.Room.API.Core.Interface;
using System;
using System.Collections.Generic;
using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Globalization;
using System.Linq;
using AppointmentBuddy.Core.Common.Helper;

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

            response = await _repository.GetRoomByRoomId(roomId);

            return response;
        }

        public async Task<IEnumerable<M.Room>> GetRoomByServiceId(string serviceId)
        {
            IEnumerable<M.Room> response;

            response = await _repository.GetRoomByServiceId(serviceId);

            return response;
        }

        public async Task<M.PaginatedResults<M.Room>> GetAllRooms(string desc, int page, int pageSize)
        {
            M.PaginatedResults<M.Room> response = null;

            var data = await _repository.GetAllRooms(desc);

            data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            response = new M.PaginatedResults<M.Room>(page, pageSize, data.Count(), data);

            return response;
        }

        public async Task<int> SaveRoom(M.Room room)
        {
            int success = Constants.ErrorCodes.Failure;

            var dbRoom = await _repository.GetRoomByRoomId(room.RoomId);

            if (dbRoom == null)
            {
                room.CreatedBy = room.LastUpdatedBy;
                room.CreatedById = room.LastUpdatedById;
                room.CreatedDate = DateTime.Now;
                room.LastUpdatedDate = DateTime.Now;
                room.VersionNo = 1;

                success = await _repository.SaveRoom(room);
            }
            else
            {
                room.LastUpdatedDate = DateTime.Now;
                room.VersionNo = dbRoom.VersionNo++;

                success = await _repository.UpdateRoom(room);
            }

            return success;
        }
    }
}
