using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.ProjectViewModels
{
    public class DeleteProjectViewModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long? CustomerID { get; set; }
        public Customer customer { get; set; }
        public bool decisionControll { get; set; }
        public List<ProjectPerson> ProjectPersons { get; set; }
    }
}
