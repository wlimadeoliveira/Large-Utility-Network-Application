using Angle.Models.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Attribute = LUNA.Models.Models.Attribute;
namespace LUNA.Models
{
    public class Product
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public long? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public ProjectModel Project { get; set; }
        public long? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
        public long? StockInformationID { get; set; }
        [ForeignKey("StockInformationID")]
        public StockInformation StockInformation { get; set; }
        public string SerialNumber { get; set; }
        public long DeviceTypeID { get; set; }
        [ForeignKey("DeviceTypeID")]
        public PType Type { get; set; }
        public long? ParentID { get; set; }
        [ForeignKey("ParentID")]
        public Product Parent { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
        public List<ProductHistory> ProductHistories { get; set; } = new List<ProductHistory>();
        public List<ProductType> ProductTypes { get; set; } = new List<ProductType>();
        public List<QuickAdventure> QuickAdventures { get; set; } = new List<QuickAdventure>();
    }
}
