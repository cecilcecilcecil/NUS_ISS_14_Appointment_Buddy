using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBuddy.Core.Model
{
    [Table("Appointment", Schema = "dbo")]
    public class Appointment
    {
        public string AppointmentId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string UserId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ServiceId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string SpecialistId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string RoomId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        [Column(TypeName = "varchar(16)")]
        public string AppointmentTime { get; set; }
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

        [NotMapped]
        public string ServiceName { get; set; }
        [NotMapped]
        public string RoomName { get; set; }
        [NotMapped]
        public string SpecialistName { get; set; }
    }
}
