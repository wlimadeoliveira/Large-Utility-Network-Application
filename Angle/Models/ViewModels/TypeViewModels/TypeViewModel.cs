
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.TypeViewModels
{
    public class TypeViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductType> ProductType { get; set; } = new List<ProductType>();
        public List<TypeFeature> TypeFeature { get; set; } = new List<TypeFeature>();
        public List<TypeAttribute> TypeAttribute { get; set; } = new List<TypeAttribute>();
       
    }
}
