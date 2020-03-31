using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.ProductHistoryViewModels
{
    public class ProductHistoryViewModelSimple
    {
        public ProductHistory History { get; set; }
        public string CreatorEmail { get; set; }
    }
}
