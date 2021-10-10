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
        }

        public static class Identity
        {
            public static string BaseURI { get; set; }
            public static string APIVersion { get; set; }

            public static string AuthenticateAPI(string api) => $"{BaseURI}/v{APIVersion}{api}";
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

            public static string RoomAPI(string api, string apptId) => $"{BaseURI}/v{APIVersion}{api}/{apptId}";
        }
    }
}
