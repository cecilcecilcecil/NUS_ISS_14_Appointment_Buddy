using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppointmentBuddy.Core.Model
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        [Column(TypeName = "varchar(50)")]
        public string UserId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string UserLogin { get; set; }
        [Column(TypeName = "varchar(16)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string UserTypeId { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Nric { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string PhoneNo { get; set; }
        public bool IsDeleted { get; set; }
        public int? VersionNo { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
