using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Room.API.Core.Interface
{
    public interface IRoomRepositoryService
    {
        Task<M.Room> GetRoomByRoomId(string roomId);
        Task<M.PaginatedResults<M.Room>> GetAllRooms(string specialiesId);
        Task<int> SaveRoom(M.Room room);
        Task<int> UpdateRoom(M.Room room);
    }
}
