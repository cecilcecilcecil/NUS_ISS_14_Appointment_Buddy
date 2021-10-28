using AppointmentBuddy.Core.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Models
{
    public class Appointment
    {
        [Required]
        public string AppointmentId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        [DisplayName("Appointment Date")]
        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        [RegularExpression(Constants.RegularExpressions.DateInddMMyyyy, ErrorMessage = Constants.ValidationMessages.DateFormatMessage)]
        [MaxLength(10)]
        public string AppointmentDate { get; set; }

        [DisplayName("Appointment Time")]
        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        [MaxLength(5)]
        [RegularExpression(Constants.RegularExpressions.TimeInHHMM, ErrorMessage = Constants.ValidationMessages.TimeFormatMessage)]
        public string AppointmentTime { get; set; }

        public string NewAppointmentId { get; set; }
    }
}
