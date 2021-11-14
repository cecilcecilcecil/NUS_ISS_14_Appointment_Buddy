using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBuddy.Core.Model
{
    [Table("Services", Schema = "dbo")]
    public class Services
    {
        public string ServicesId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; }
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
