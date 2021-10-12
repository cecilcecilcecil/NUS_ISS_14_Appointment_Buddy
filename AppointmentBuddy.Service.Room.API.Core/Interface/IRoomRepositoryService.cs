using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Room.API.Core.Interface
{
    public interface IRoomRepositoryService
    {
        Task<M.Room> GetRoomByRoomId(string roomId);
    }
}
