using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentBuddy.Core.Model
{
    public class FilteredAppointment
    {
        public List<string> PatientIds { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string AppointmentTime { get; set; }
    }
}
