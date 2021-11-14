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
        public string RoomName
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
        public bool IsAvailable
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
        public string CreatedById
        {
            get; set;
        }
        public string CreatedBy
        {
            get; set;
        }
        public DateTime? CreatedDate
        {
            get; set;
        }
        [Column(TypeName = "varchar(255)")]
        public string LastUpdatedById
        {
            get; set;
        }
        public string LastUpdatedBy
        {
            get; set;
        }
        public DateTime? LastUpdatedDate
        {
            get; set;
        }

        [NotMapped]
        public string ServicesName { get; set; }
    }
}
