using Angle.Interfaces;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Attribute = LUNA.Models.Models.Attribute;
using Angle.Models.ViewModels.AttributeViewModel;

namespace Angle.Repositories
{
    public class AttributeRepo : IAttribute
    {
        private readonly ProjectDataContext _context;
        public AttributeRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void Delete(Attribute attribute)
        {
            _context.Attribute.Remove(attribute);
        }

     

        public List<Attribute> GetAll()
        {
            return _context.Attribute.ToList();
        }

        public Attribute GetById(long Id)
        {
            return _context.Attribute.FirstOrDefault(x => x.ID == Id);
        }

        public Attribute GetByName(string name)
        {
            return _context.Attribute.FirstOrDefault(x => x.Name == name);
        }

        public void Insert(Attribute attribute)
        {
            _context.Attribute.Add(attribute);

        }

        public Attribute GetAttributeForDelete(long Id)
        {
            return _context.Attribute.Include(x => x.ProductAttributes).Include(y => y.TypeAttributes).FirstOrDefault(z => z.ID == Id);
        }
        

        public void Update(Attribute attribute)
        {
            _context.Attribute.Update(attribute);
        }


    }
}
