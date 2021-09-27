using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GEMS2.Core.Model
{
    [Table("Code", Schema = "dbo")]
    public class Code
    {
        [Column(TypeName = "varchar(50)")]
        public string CodeId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Category { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string CodeValue { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string ShortDescription { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string LongDescription { get; set; }
        public int? SequenceNo { get; set; }
        public bool? IsDeleted { get; set; }
        public int? VersionNo { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
