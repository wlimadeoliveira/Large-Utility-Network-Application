
using Angle.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class PType
    {
        public long ID { get; set; }
     
        public string Name { get; set; }
        public string Description { get; set; }
        //public long? ParentID { get; set; }

       // PType Parent { get; set; }


        public List<ProductType> ProductTypes { get; set; } = new List<ProductType>();
        public List<TypeFeature> TypeFeatures { get; set; } = new List<TypeFeature>();
        public List<TypeAttribute> TypeAttributes { get; set; } = new List<TypeAttribute>();
      // public List<TypeChild> Parents { get; set; } = new List<TypeChild>();
        public List<TypeChild> Childs { get; set; } = new List<TypeChild>();



    }



}
