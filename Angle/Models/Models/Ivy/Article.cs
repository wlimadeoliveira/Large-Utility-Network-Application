using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models.Ivy
{
    public class Article
    {
        [Key]
        public long ID {get;set;}
        public long CategoryID { get; set; }
        public string Description { get; set; }
        public double Pricing { get; set; }
        public long Quantity { get; set; }
        public string SupplierPartNumber { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public long? ManufacturerID { get; set; }
        public long? SupplierID { get; set; }     
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public long? LocationID { get; set; }
        [ForeignKey("ManufacturerID")]
        Manufacturer Manufacturer { get; set; }
        [ForeignKey("SupplierID")]
        Supplier Supplier { get; set; }
        [ForeignKey("LocationID")]
        Location Location { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
        public List<ProjectArticle> ProjectArticles { get; set; }





    }
}
