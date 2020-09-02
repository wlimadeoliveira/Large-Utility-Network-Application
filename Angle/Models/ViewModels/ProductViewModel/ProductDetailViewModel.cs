using Angle.Models.Models;
using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.ProductViewModel
{
    public class ProductDetailViewModel
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public long? ProjectID { get; set; }
        public ProjectModel Project { get; set; }
        public long? CustomerID { get; set; }
        public Customer Customer { get; set; }
        public long? StockInformationID { get; set; }
        public StockInformation StockInformation { get; set; }
        public string SerialNumber { get; set; }
        public long DeviceTypeID { get; set; }
        public PType Type { get; set; }
        public long? ParentID { get; set; }
        public Product Parent { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
        public List<ProductHistory> ProductHistories { get; set; } = new List<ProductHistory>();
        public List<ProductType> ProductTypes { get; set; } = new List<ProductType>();
        public List<ProductSoftwareOptions> ProductSoftwareOptions { get; set; } = new List<ProductSoftwareOptions>();
        public SoftwareType SoftwareType { get; set; }



      



    }
}
