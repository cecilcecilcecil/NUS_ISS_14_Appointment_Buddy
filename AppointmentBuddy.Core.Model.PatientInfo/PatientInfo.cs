using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBuddy.Core.Model
{
    [Table("PatientInfo", Schema = "dbo")]
    public class PatientInfo
    {
        public string PatientId { get; set; }
        public string NRIC { get; set; }
        public string PatientName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string ContactNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedById { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastUpdatedById { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
