using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models
{
    public class SoftwareTypeOptions
    {
        [Key]
        public long ID { get; set; }
        public long SoftwareTypeID { get; set; }
        public long SoftwareOptionID { get; set; }
        [ForeignKey("SoftwareTypeID")]
        public SoftwareType SoftwareType { get; set; }
        [ForeignKey("SoftwareOptionID")]
        public SoftwareOption SoftwareOption {get;set;}
        public string Value { get; set; }
    }
}
