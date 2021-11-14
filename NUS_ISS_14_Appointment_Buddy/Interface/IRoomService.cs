using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;


namespace NUS_ISS_14_Appointment_Buddy.Interface
{
    public interface IRoomService
    {
        Task<M.Room> GetRoomByRoomId(string roomId, string token);
        Task<IEnumerable<M.Room>> GetRoomByServiceId(string serviceId, string token);
        Task<M.PaginatedResults<M.Room>> GetAllRooms(string token, string desc, int page, int pageSize);


        Task<int> SaveRoom(M.Room room, string token);
    }
}
