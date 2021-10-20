using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBuddy.Core.Model
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
        public int Services { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public bool? Available { get; set; }
        [Required]
        public String Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        public String CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public String LastUpdatedBy { get; set; } = "System";
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        [Required]
        public bool? IsDeleted { get; set; } = false;
    }

    public class Service
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string LastUpdatedBy { get; set; } = "System";
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public Boolean IsDeleted { get; set; }
    }

    public class Available
    {
        public string Value { get; set; }
        public string Text { get; set; }

    }

    public class CollectionDataModel
    {
        //public List<Specialist> Specialist { get; set; }
        //public List<Service> Service { get; set; }
        public Specialist modelSpec { get; set; }

        public List<Service> Service { get; set; }

        public List<Available> Available { get; } = new List<Available>
        {
             new Available { Value = "true", Text = "Yes" },
             new Available { Value = "false", Text = "No" }
        };
    }
}
