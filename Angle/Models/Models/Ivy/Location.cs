using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models.Ivy
{
    public class Location
    {
        [Key]
        public long ID { get; set; }
        public string Description { get; set; }
    }
}
