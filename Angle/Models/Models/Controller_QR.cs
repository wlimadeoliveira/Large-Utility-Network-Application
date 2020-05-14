using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class Controller_QR
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public List<Index_QR> Index_QRs { get; set; } = new List<Index_QR>();
    }
}
