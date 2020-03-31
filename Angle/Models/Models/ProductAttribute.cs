using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attribute = LUNA.Models.Models.Attribute;

namespace LUNA.Models.Models
{
    public class ProductAttribute
    {
   
        public long ProductID { get; set; }
        public long AttributeID { get; set; }
        public string Value { get; set; }
        public Product Product { get; set; }
        public Attribute Attribute { get; set; }
        
      
    }
}
