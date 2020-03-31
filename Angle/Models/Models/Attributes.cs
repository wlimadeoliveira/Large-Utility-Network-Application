using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class Attribute
    {
        public long ID { get; set; }
        public string Name { get; set; } //Name of attribute p.E. Color, Material etc...
        public List<TypeAttribute> TypeAttributes { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
    }
}
