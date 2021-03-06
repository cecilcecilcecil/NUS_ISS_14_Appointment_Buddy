using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Helper
{
    public static class UrlConfig
    {
        public static class Appointment
        {
            public static string BaseURI { get; set; }
            public static string APIVersion { get; set; }

            public static string AppointmentAPI(string api, string apptId) => $"{BaseURI}/v{APIVersion}{api}/{apptId}";
            public static string AllAppointmentAPI(string api, string parameter) => $"{BaseURI}/v{APIVersion}{api}?{parameter}";
            public static string GetAvailableAppointmentsAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
            public static string GetAllAppointmentsByDateRangeAPI(string api, string dateFrom, string dateTo) => $"{BaseURI}/v{APIVersion}{api}?dateFrom={dateFrom}&dateTo={dateTo}";
            public static string SearchAppointmentAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
            public static string SaveAppointmentAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
        }

        public static class Services
        {
            public static string BaseURI { get; set; }
            public static string APIVersion { get; set; }

            public static string ServiceAPI(string api, string svcId) => $"{BaseURI}/v{APIVersion}{api}/{svcId}";
            public static string GetAllNonPageServicesAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
            public static string AllServiceAPI(string api, string parameter) => $"{BaseURI}/v{APIVersion}{api}?{parameter}";
            public static string SaveServicesAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
        }

        public static class Specialist
        {
            public static string BaseURI { get; set; }
            public static string APIVersion { get; set; }

            public static string SpecialistAPI(string api, string specId) => $"{BaseURI}/v{APIVersion}{api}/{specId}";
            public static string SpecialistByServiceAPI(string api, string serviceId) => $"{BaseURI}/v{APIVersion}{api}/{serviceId}";
            public static string SaveSpecialistAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
            public static string SearchSpecialistAPI(string api, string parameter) => $"{BaseURI}/v{APIVersion}{api}?{parameter}";
        }

        public static class PatientInfo
        {
            public static string BaseURI { get; set; }
            public static string APIVersion { get; set; }

            public static string PatientInfoAPI(string api, string patId) => $"{BaseURI}/v{APIVersion}{api}/{patId}";
            public static string PatientInfoByUserAPI(string api, string userId) => $"{BaseURI}/v{APIVersion}{api}/{userId}";
            public static string SavePatientInfoAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
            public static string SearchPatientInfoAPI(string api, string parameter) => $"{BaseURI}/v{APIVersion}{api}?{parameter}";
        }

        public static class Identity
        {
            public static string BaseURI { get; set; }
            public static string APIVersion { get; set; }

            public static string AuthenticateAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
            public static string PatientsAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
            public static string SavePatientAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
        }

        public static class Room
        {
            public static string BaseURI
            {
                get; set;
            }
            public static string APIVersion
            {
                get; set;
            }

            public static string RoomAPI(string api, string roomId) => $"{BaseURI}/v{APIVersion}{api}/{roomId}";
            public static string RoomByServiceAPI(string api, string serviceId) => $"{BaseURI}/v{APIVersion}{api}/{serviceId}";
            public static string AllRoomAPI(string api, string parameter) => $"{BaseURI}/v{APIVersion}{api}?{parameter}";
            public static string SaveRoomAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
        }
    }
}
