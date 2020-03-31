using Angle.Models.Models.Ivy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LUNA.Models.Models
{
    public class ProjectModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
        public ICollection<ProjectPerson> ProjectPersons { get; set; }
        public List<ProjectArticle> ProjectArticles { get; set; }
       

    }
}
