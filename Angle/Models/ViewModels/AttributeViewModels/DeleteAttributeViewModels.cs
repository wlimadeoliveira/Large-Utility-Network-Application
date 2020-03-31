using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.AttributeViewModel
{
    public class DeleteAttributeViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<Product> Products {get;set;}
        public List<PType> Types { get; set; }
        public List<string> containingTypes { get; set; } = new List<string>();
        public List<string> containingProducts { get; set; } = new List<string>();
        public bool smartControll { get; set; }
        public bool decisionControll { get; set; }
       
       

    }
}
