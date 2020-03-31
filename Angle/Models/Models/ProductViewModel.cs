using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class ProductViewModel
    {
        public long ProductID { get; set; }
        public string SerialNumber { get; set; }
        public string ProductDescription { get; set; }
        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public string TypeName { get; set; }
        public long? ParentID { get; set; }
        public string ParentSerialNumber { get; set; }
    }
}
