using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models
{
    public class SoftwareOption
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public string DataTypeValue { get; set; }
        public List<SoftwareTypeOptions> SoftwareTypeOptions { get; set; }

    }
}
