using Angle.Interfaces;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace Angle.Models.ViewModels.ProductViewModel
{
    public class ProductCreateViewModel
    {  
        public long ID { get; set; }
        public string Description { get; set; }
        public long? ProjectID { get; set; }
        public long? CustomerID { get; set; }
        public long? StockInformationID { get; set; }
        [Remote(action: "SerialNumberExist", controller:"Product")]
        public string SerialNumber { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Must Select A Value")]
        public long DeviceTypeID { get; set; }
        public long? ParentID { get; set; }
        public PType Type { get; set; }   
        public string Email { get; set; }
        public string[] TypeChild { get; set; }
        public string[] SelectedAttributes{ get; set; }
        public string[] ValueSelectedAttributes { get; set; }      
        
    }
}
