using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.ProductHistoryViewModels
{
    public class ProductHistoryViewModelDetail
    {
        public ProductHistory History { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
        public long HistoryTypeID { get; set; }
        public long ProductId { get; set; }
    }
}
