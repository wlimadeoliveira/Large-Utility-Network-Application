using Angle.Models.ViewModels.AccountViewModel;
using LUNA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class QuickAdventure
    {   
        public string UserID { get; set; }
        public long ProductID { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}
