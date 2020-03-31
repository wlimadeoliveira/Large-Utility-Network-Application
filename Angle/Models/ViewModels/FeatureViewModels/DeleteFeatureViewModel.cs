using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.FeatureViewModels
{
    public class DeleteFeatureViewModel
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        
        public bool decisionControll { get; set; }
        public List<string> typeNames { get; set; } = new List<string>();

    }
}
