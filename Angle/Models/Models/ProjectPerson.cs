using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class ProjectPerson
    {
   
        public long ProjectID { get; set; }
        public ProjectModel Project { get; set; }
        public long PersonID { get; set; }
        public Person Person { get; set; }
    }
}
