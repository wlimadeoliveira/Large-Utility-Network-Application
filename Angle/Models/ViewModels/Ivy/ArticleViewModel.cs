using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.Ivy
{
    public class ArticleViewModel
    {
        public long ID { get; set; }
        public long CategoryID { get; set; }
        public string Description { get; set; }
        public double Princing { get; set; }
        public int Quantity { get; set; }
        public string SupplierPartNumber { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public long? ManufacturerID { get; set; }
        public long? SupplierID { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdate { get; set; }
        public long LocationID { get; set; }




    }
}
