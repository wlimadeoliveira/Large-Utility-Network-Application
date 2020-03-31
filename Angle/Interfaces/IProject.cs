using LUNA.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IProject
    {
        List<ProjectModel> GetAll();
        ProjectModel GetById(long Id);
        ProjectModel GetByName(string name);
        void Insert(ProjectModel project);
        void Update(ProjectModel project);
        void Delete(ProjectModel project);
    }
}
