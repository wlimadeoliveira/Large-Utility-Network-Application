using Angle.Interfaces;
using Angle.Models.Models.Ivy;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories.Ivy
{
    public class SupplierRepo : ISupplier
    {

        private readonly ProjectDataContext _context;
        public SupplierRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void Delete(Supplier Supplier)
        {
            throw new NotImplementedException();
        }

        public List<Supplier> GetAll()
        {
            return _context.Supplier.ToList();
        }

        public Supplier GetById(long Id)
        {
           return _context.Supplier.FirstOrDefault(x => x.ID == Id);
        }

        public Supplier GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Supplier GetSupplierForDelete(long Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Supplier Supplier)
        {
            _context.Supplier.Add(Supplier);
        }

        public void Update(Supplier Supplier)
        {
            throw new NotImplementedException();
        }
    }
}
