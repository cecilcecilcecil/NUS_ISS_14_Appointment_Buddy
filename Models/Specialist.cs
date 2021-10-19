using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIConsume.Models
{
    public class Specialist
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NRIC { get; set; }
        public string ServiceDescription { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public bool? Available { get; set; }
        [Required]
        public String Address { get; set; }
        public String Email { get; set; }
        public String CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public String LastUpdatedBy { get; set; } = "System";
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        [Required]
        public bool? IsDeleted { get; set; } = false;
    }
}
