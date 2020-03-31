using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models
{
    public class TypeChild
    {
        public long TypeID { get; set; }
        public long ChildID { get; set; }
        public long Quantity { get; set; }
        public PType Type { get; set; }
        public PType Child { get; set; }

    }
}
