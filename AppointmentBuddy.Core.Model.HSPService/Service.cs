using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBuddy.Core.Model
{
    public class Services
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public string CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string LastUpdatedBy { get; set; } = "System";
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public bool? IsDeleted { get; set; } = false;
    }
}
