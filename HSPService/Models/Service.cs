using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSPService.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}

