using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentBuddy.Service.Room.API.Core.Interface
{
    public interface IRoomRepositoryService
    {
        Task<M.Room> GetRoomByRoomId(string roomId);
        Task<IEnumerable<M.Room>> GetAllRooms(string specialiesId);
        Task<int> SaveRoom(M.Room room);
        Task<int> UpdateRoom(M.Room room);
    }
}
