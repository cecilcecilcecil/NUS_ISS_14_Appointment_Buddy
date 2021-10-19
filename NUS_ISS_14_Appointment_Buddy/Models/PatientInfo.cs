using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NUS_ISS_14_Appointment_Buddy.Models
{
    public class PatientInfo
    {
        public string PatientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "NRIC is Required")]
        public string NRIC { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Patient Name is Required")]
        public string PatientName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string ContactNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastUpdatedById { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
