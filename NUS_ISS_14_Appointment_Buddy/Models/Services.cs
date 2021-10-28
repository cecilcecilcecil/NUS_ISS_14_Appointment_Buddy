using AppointmentBuddy.Core.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Models
{
    public class Services
    {
        [Required]
        public string ServicesId { get; set; }
        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
