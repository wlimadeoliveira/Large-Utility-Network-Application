using Angle.Models.Models;
using Angle.Models.ViewModels.AccountViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace LUNA.Models.Models
{
    public class ProductHistory
    {
        public long ProductID { get; set; }
        public long HistoryID { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public Product Product { get; set; }    
        public History History { get; set; }
        public string UserID { get; set; }
        [ForeignKey("File")]
        public long? FileID { get; set; }
        public Upload File{ get; set; }
        public ApplicationUser User { get; set; }
    }
}
