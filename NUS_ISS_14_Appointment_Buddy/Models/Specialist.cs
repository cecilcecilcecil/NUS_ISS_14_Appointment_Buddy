using AppointmentBuddy.Core.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Models
{
    public class Specialist
    {
        public string SpecialistId { get; set; }
        public string ServicesId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "NRIC is Required")]
        public string NRIC { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Specialist Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        [MaxLength(8)]
        [RegularExpression(Constants.RegularExpressions.IsSGPhone, ErrorMessage = Constants.ValidationMessages.PhoneFormatMessage)]
        public string ContactNo { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        [MaxLength(64)]
        [RegularExpression(Constants.RegularExpressions.IsEmailLocal, ErrorMessage = Constants.ValidationMessages.EmailFormatMessage)]
        public string EmailLocalPart { get; set; }

        [DisplayName("Email Domain")]
        [Required(ErrorMessage = Constants.ValidationMessages.RequiredMessage)]
        [RegularExpression(Constants.RegularExpressions.IsEmailDomain, ErrorMessage = Constants.ValidationMessages.EmailDomainFormatMessage)]
        [MaxLength(255)]
        public string EmailDomain { get; set; }

        public bool Availability { get; set; }
    }
}
