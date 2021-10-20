﻿using System;
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
        public string AppointmentAPI_SaveAppointment { get; set; }
        public string AppointmentAPI_GetFilteredAppointmentsByPatientIds { get; set; }

        public string PatientInfoAPI_GetPatientInfoById { get; set; }
        public string PatientInfoAPI_GetPatientInfoBySearch { get; set; }
        public string PatientInfoAPI_DeletePatientInfoById { get; set; }
        public string PatientInfoAPI_DeactivatePatientInfoById { get; set; }
        public string PatientInfoAPI_SavePatientInfo{ get; set; }
        public string IdentityAPI { get; set; }
        public string IdentityAPIVersion { get; set; }

        public string IdentityAPI_Authenticate { get; set; }
        public string IdentityAPI_PatientsAPI { get; set; }

        public string RoomAPI { get; set; }
        public string RoomAPIVersion { get; set; }
        public string RoomAPI_GetRoomByRoomId { get; set; }

    }
}
