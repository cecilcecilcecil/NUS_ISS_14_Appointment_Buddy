using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBuddy.Core.Model
{
    [Table("Specialist", Schema = "dbo")]
    public class Specialist
    {
        public string SpecialistId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ServicesId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Nric { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string ContactNo { get; set; }
        [Column(TypeName = "varchar(64)")]
        public string EmailLocalPart { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string EmailDomain { get; set; }
        public bool Availability { get; set; }
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
