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
        Task<M.PaginatedResults<M.Room>> GetAllRooms(string token, string specialtiesId, int page, int pageSize);


        Task<int> SaveRoom(M.Room room, string token);
    }
}
