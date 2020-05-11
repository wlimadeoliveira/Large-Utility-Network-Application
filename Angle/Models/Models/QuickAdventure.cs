using Angle.Models.ViewModels.AccountViewModel;
using LUNA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class QuickAdventure
    {   
        [Key]
        public long ID { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public long ProductID { get; set; }
        [ForeignKey("ProductID")]
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}
