using M = AppointmentBuddy.Core.Model;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Room.API.Core.Interface
{
    public interface IRoomService
    {
        Task<M.Room> GetRoomByRoomId(string roomId);
    }
}
