﻿using Angle.Models.ViewModels.AccountViewModel;
using LUNA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Angle.Models.Models
{
    // Model for Index_QR, It's also used to generate Database Entities with EntityFrameWork Core
    // Composite Key: ActionID, ControllerID, ProductID
    public class Index_QR
    {
        [ForeignKey("Action")]
        public long ActionID { get; set; }

        [ForeignKey("Controller")]
        public long ControllerID { get; set; }
        
        [ForeignKey("Product")]
        public long ProductID { get; set; }
        
       [ForeignKey("User")]
        public string UserID { get; set; }
       
        public string Parameter { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public Action_QR Action_QR { get; set; }
        public Controller_QR Controller_QR {get;set;}
        public Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
       
    }
}