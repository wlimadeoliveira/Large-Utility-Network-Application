using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.ProductViewModel
{
    public class ProductViewModel
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public long? ProjectID { get; set; }
        public long? CustomerID { get; set; }
        public long? StockInformationID { get; set; }
        public string SerialNumber { get; set; }
        public string TypeName { get; set; }
        public long? DeviceTypeID { get; set; }
        public string ParentName { get; set; }
        public long? ParentID { get; set; }
        public string ProjectName { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public string[] TypeChild { get; set; }
    }
}
