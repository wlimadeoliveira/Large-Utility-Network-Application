using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models.Ivy
{
    public class ProjectArticle
    {
        public long ProjectID { get; set; }
        public long ArticleID { get; set; }
        public ProjectModel Project { get; set; }
        public Article Article { get; set; }

    }
}
