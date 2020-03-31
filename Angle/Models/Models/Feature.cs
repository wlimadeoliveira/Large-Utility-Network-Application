using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class Feature
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<TypeFeature> TypeFeature { get; set; }
    }
}
