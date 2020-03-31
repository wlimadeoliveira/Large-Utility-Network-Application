using Angle.Interfaces;
using Angle.Models.Models.Ivy;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories.Ivy
{
    public class CategoryRepo : ICategory
    {

        private readonly ProjectDataContext _context;
        public CategoryRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void Delete(Category Category)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            return _context.Category.ToList();
        }

        public Category GetById(long Id)
        {
           return _context.Category.FirstOrDefault(x => x.ID == Id);
        }

        public Category GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryForDelete(long Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Category Category)
        {
            _context.Category.Add(Category);
        }

        public void Update(Category Category)
        {
            throw new NotImplementedException();
        }
    }
}
