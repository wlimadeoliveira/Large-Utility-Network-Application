using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models.Models;
using Angle.Models.Models.Ivy;
namespace Angle.Interfaces
{
    public interface ICategory
    {
        List<Category> GetAll();
        Category GetById(long Id);
        Category GetByName(string name);
        void Insert(Category Category);
        void Update(Category Category);
        void Delete(Category Category);
        Category GetCategoryForDelete(long Id);
    }
}
