using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models.Ivy
{

    //NULL und Defautvalues setzen bei neiue einträge
    public class Article
    {
        [Key]
        public long ID { get; set; }
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
        //Altium Expansion
        /*
        [Column(TypeName = "VARCHAR(50)")]
        public string AD_ID { get; set; }
        
        [Column(TypeName = "TINYTEXT")]
  public string  AD_LibraryRef { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string  AD_FootprintRef1 { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string  AD_FootprintRef2 { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string   AD_Comment { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string   AD_Description { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string   AD_Description1 { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string  AD_PCW_PartNr { get; set; }
        [Column(TypeName = "TEXT")]
        public Substitutable AD_Substitutable { get; set; }
        [Column(TypeName = "TEXT")]
        public string   AD_HelpURL { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string  AD_Tolerance { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string   AD_Value { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string AD_KeyRating { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string  AD_Package { get; set; }
        //[Column(TypeName = "TEXT")]
        public Mounting? AD_Mounting { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string   AD_Manufacturer1 { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string   AD_ManufacturerPartNumber1 { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string  AD_Manufacturer2 { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string   AD_ManufacturerPartNumber2 { get; set; }
        [Column(TypeName = "TEXT")]
        public string  AD_LifeCycle { get; set; }
        [Column(TypeName = "CHAR(10)")]
        public LifeCycleVisum  AD_LifeCycleVisum { get; set; }
        [Column(TypeName = "CHAR(10)")]
        public string   AD_Author { get; set; }
        [Column(TypeName = "CHAR(10)")]
        public string   AD_Reviewer { get; set; }
        [Column(TypeName = "CHAR(10)")]
        public string   AD_Validator { get; set; }
        [Column(TypeName = "TINYTEXT")]
        public string   AD_FirstProject { get; set; }
        [Column(TypeName = "VARCHAR(2)")]
        public string   AD_Designator { get; set; }
        [Column(TypeName = "TEXT")]
        public AltiumComponent AD_AltiumComponent { get; set; }

     
        */


    /*
    public enum Substitutable
    {
        Yes,
        No,
    }

    public enum Mounting
    {
        NotSet,
        SMD,
        THT,
        Mech,

    }

    public enum LifeCycleVisum
    {
        ACTIVE,
        DISCOUNTINUED,
        OBSOLETE,
    }

    public enum AltiumComponent
    {
        True,
        False
    } */

}
