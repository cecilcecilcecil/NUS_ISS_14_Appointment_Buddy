using AppointmentBuddy.Core.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Models
{
    public class Room
    {
        [Required]
        public string RoomId { get; set; }

        [DisplayName("Room Name")]
        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        public string RoomName { get; set; }
        public string SpecialiesId { get; set; }
        public bool isAvailable { get; set; }
    }
}
