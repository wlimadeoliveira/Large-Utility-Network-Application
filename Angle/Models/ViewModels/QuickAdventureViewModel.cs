using Angle.Models.ViewModels.AccountViewModel;
using LUNA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels
{
    public class QuickAdventureViewModel
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public long ProductID { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}
