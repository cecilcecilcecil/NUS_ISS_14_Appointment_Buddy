using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppointmentBuddy.Core.Model
{
    [Table("UserRole", Schema = "dbo")]
    public class UserRole
    {
        [Column(TypeName = "varchar(50)")]
        public string UserRoleId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string UserId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string RoleId { get; set; }
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
