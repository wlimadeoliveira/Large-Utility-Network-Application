using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models
{
    public class SoftwareType
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public List<SoftwareTypeOptions> SoftwareTypeOptions { get; set; }
        public List<ProductSoftwareOptions> ProductSoftwareOptions { get; set; }
    }
}
