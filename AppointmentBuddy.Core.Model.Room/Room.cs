using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBuddy.Core.Model
{
    public class Room
    {
        [Column(TypeName = "varchar(50)")]
        public string RoomId
        {
            get; set;
        }
        [Column(TypeName = "varchar(50)")]
        public string SpecialiesId
        {
            get; set;
        }
        [Column(TypeName = "varchar(50)")]
        public string ServicesId
        {
            get; set;
        }
        [Column(TypeName = "varchar(255)")]
        public string Availabilities
        {
            get; set;
        }
        public bool IsDeleted
        {
            get; set;
        }
        public int? VersionNo
        {
            get; set;
        }
        [Column(TypeName = "varchar(255)")]
        public string CreatedBy
        {
            get; set;
        }
        public DateTime? CreatedDate
        {
            get; set;
        }
        [Column(TypeName = "varchar(255)")]
        public string LastUpdatedBy
        {
            get; set;
        }
        public DateTime? LastUpdatedDate
        {
            get; set;
        }
    }
}
