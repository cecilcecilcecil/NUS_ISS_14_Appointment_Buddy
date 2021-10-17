using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSPSpecialist.Models
{
    public class Specialist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NRIC { get; set; }
        public int Services { get; set; }
        public int Contact { get; set; }
        public Boolean Available { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public string ServiceDescription { get; set; }
    }
    public class Service
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
