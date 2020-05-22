using Angle.Models.ViewModels.AccountViewModel;
using LUNA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace LUNA.Models.Models
{
    // Model for Index_QR, It's also used for Migration and Database Entities Queries with EntityFrameWork Core
    //
    public class Index_QR
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("ActionID")]
        public long ActionID { get; set; }
        [ForeignKey("ControllerID")]
        public long ControllerID { get; set; }
        [ForeignKey("ProductID")]
        public long ProductID { get; set; }
        [ForeignKey("UserID")]
        public string UserID { get; set; }
        public string Parameter { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public Action_QR Action_QR { get; set; }
        public Controller_QR Controller_QR { get; set; }
        public Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
