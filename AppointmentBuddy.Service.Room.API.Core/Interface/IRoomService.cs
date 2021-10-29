using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Room.API.Core.Interface
{
    public interface IRoomService
    {
        Task<M.Room> GetRoomByRoomId(string roomId);
        Task<IEnumerable<M.Room>> GetRoomByServiceId(string serviceId);
        Task<M.PaginatedResults<M.Room>> GetAllRooms(string specialiesId, int page, int pageSize);
        Task<int> SaveRoom(M.Room room);
    }
}
