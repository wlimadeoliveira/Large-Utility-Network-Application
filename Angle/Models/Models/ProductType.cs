using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class ProductType
    {
    
         
        public string Name { get; set; }
        
        public long ProductID { get; set; }
        public long DeviceTypeID { get; set; }
        public Product Product { get; set; }
        public PType DeviceType { get; set; }
    }
}
