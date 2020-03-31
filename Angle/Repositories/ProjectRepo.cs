using Angle.Interfaces;
using LUNA.Models.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories
{
    public class ProjectRepo : IProject
    {
        private readonly ProjectDataContext _context;
        public ProjectRepo(ProjectDataContext context)
        {
            _context = context;
        }


        public void Delete(ProjectModel project)
        {
            _context.Projects.Remove(project);
        }

        public List<ProjectModel> GetAll()
        {
            return _context.Projects.Include(x => x.Customer).ToList();
        }

        public ProjectModel GetById(long Id)
        {
            return _context.Projects.Include(y => y.Customer).FirstOrDefault(x => x.ID == Id);
        }

        public ProjectModel GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(ProjectModel project)
        {
            _context.Projects.Add(project);
        }

        public void Update(ProjectModel project)
        {
            _context.Projects.Update(project);
        }
    }
}
