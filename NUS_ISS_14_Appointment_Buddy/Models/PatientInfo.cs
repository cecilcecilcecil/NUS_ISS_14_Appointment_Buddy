using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AppointmentBuddy.Core.Common.Helper;
using System.ComponentModel;

namespace NUS_ISS_14_Appointment_Buddy.Models
{
    public class PatientInfo
    {
        public string PatientId { get; set; }
        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "NRIC is Required")]
        public string NRIC { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Patient Name is Required")]
        public string PatientName { get; set; }

        [DisplayName("Birth Date")]
        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        [RegularExpression(Constants.RegularExpressions.DateInddMMyyyy, ErrorMessage = Constants.ValidationMessages.DateFormatMessage)]
        public DateTime BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }

        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        [MaxLength(8)]
        [RegularExpression(Constants.RegularExpressions.IsSGPhone, ErrorMessage = Constants.ValidationMessages.PhoneFormatMessage)]
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
