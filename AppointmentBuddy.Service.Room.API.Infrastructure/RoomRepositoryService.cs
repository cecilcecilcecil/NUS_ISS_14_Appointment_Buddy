using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Room.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Room.API.Infrastructure
{
    public class RoomRepositoryService : IRoomRepositoryService
    {
        private readonly AppointmentBuddyDBContext _context;
        private readonly ILogger<RoomRepositoryService> _logger;

        public RoomRepositoryService(AppointmentBuddyDBContext context, ILogger<RoomRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<M.Room> GetRoomByRoomId(string roomId)
        {
            M.Room dataItem;

            dataItem = await _context.Room.AsNoTracking().FirstOrDefaultAsync(s => s.RoomId == roomId);
            return dataItem;
        }

        public async Task<IEnumerable<M.Room>> GetAllRooms(string desc)
        {
            if (string.IsNullOrEmpty(desc))
            {
                return await _context.Room.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.RoomName)
                .ToListAsync();
            }
            return await _context.Room.AsNoTracking()
                .Where(x => !x.IsDeleted && x.RoomName.Contains(desc))
                .OrderBy(x => x.RoomName)
                .ToListAsync();
        }

        public async Task<int> SaveRoom(M.Room room)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Add(room);
            success = await _context.SaveChangesAsync();

            return success;
        }

        public async Task<int> UpdateRoom(M.Room room)
        {
            int success = Constants.ErrorCodes.Failure;

            _context.Update(room);
            success = await _context.SaveChangesAsync();

            return success;
        }
    }
}
