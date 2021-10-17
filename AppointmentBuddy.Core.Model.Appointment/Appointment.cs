using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBuddy.Core.Model
{
    public class Appointment
    {
        public string AppointmentId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string UserId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? VersionNo { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string CreatedById { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string LastUpdatedById { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
