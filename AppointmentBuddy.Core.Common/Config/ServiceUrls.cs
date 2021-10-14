using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentBuddy.Core.Common.Config
{
    public class ServiceUrls
    {
        public string AppointmentAPI { get; set; }
        public string AppointmentAPIVersion { get; set; }

        public string AppointmentAPI_GetAppointmentByAppointmentId { get; set; }
        public string AppointmentAPI_GetAllAppointments { get; set; }

        public string IdentityAPI { get; set; }
        public string IdentityAPIVersion { get; set; }

        public string IdentityAPI_Authenticate { get; set; }

        public string RoomAPI { get; set; }
        public string RoomAPIVersion { get; set; }
        public string RoomAPI_GetRoomByRoomId { get; set; }

    }
}
