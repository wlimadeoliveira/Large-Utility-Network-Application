using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models.Ivy
{
    public class Manufacturer
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public string ClientNumber { get; set; }
        public string WebAddress { get; set; }
    }
}
