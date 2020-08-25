using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels
{
    public class SoftwareTypeViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Values{ get; set; }
        public string[] hiddenID { get; set; }
    }
}
