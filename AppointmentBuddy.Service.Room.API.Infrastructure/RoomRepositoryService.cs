using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Room.API.Core.Interface;
using AppointmentBuddy.Service.Room.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Room.API.Infrastructure
{
    public class RoomRepositoryService : IRoomRepositoryService
    {
        private readonly RoomBuddyDBContext _context;
        private readonly ILogger<RoomRepositoryService> _logger;

        public RoomRepositoryService(RoomBuddyDBContext context, ILogger<RoomRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<M.Room> GetRoomByRoomId(string roomId)
        {
            M.Room dataItem;
            dataItem = await _context.Appointment.FirstOrDefaultAsync(s => s.RoomId == roomId);
            return dataItem;
        }
    }
}
