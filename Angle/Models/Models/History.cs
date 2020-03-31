using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models.Models;

namespace LUNA.Models
{
    public class History
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public List<ProductHistory> ProductHistories { get; set; }
        public string BootStrapBadge { get; set; }
    }
}
