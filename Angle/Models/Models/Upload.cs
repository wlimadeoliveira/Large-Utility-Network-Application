using Angle.Models.ViewModels.AccountViewModel;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models
{
    public class Upload
    {
        [Key]
        public long ID { get; set; }
        public string RelativPath { get; set; }
        public string Size { get; set; }
        public string Format { get; set; }          
          
    }
}
