using Angle.Interfaces;
using Angle.Models.Models;
using LUNA.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories
{
    public class TypeRepo : IType
    {
        private readonly ProjectDataContext _context;
        public TypeRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void SetParent(long parentID,long childID)
        {

        }

        public void Delete(PType type)
        {
            _context.PType.Remove(type);
        }

        public List<PType> GetAll()
        {
            return _context.PType.Include(x => x.TypeAttributes).ToList();
        }

       
        public PType GetById(long? Id)
        {
            return _context.PType.Include(a => a.TypeAttributes).Include(b => b.TypeFeatures).Include(c => c.Childs).FirstOrDefault(x => x.ID == Id);
        }

        public PType GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(PType type)
        {
            _context.PType.Add(type);
        }

        public void Update(PType type)
        {
            _context.PType.Update(type);
        }

 



      
    }
}
