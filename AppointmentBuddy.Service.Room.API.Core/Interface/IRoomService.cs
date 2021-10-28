using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Room.API.Core.Interface
{
    public interface IRoomService
    {
        Task<M.Room> GetRoomByRoomId(string roomId);
        Task<M.PaginatedResults<M.Room>> GetAllRooms(string specialiesId, int page, int pageSize);
        Task<int> SaveRoom(M.Room room);
    }
}
