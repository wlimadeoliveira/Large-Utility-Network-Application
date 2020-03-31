using Angle.Interfaces;
using Angle.Models.Models.Ivy;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories.Ivy
{
    public class ManufacturerRepo : IManufacturer
    {

        private readonly ProjectDataContext _context;
        public ManufacturerRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void Delete(Manufacturer Manufacturer)
        {
            throw new NotImplementedException();
        }

        public List<Manufacturer> GetAll()
        {
            return _context.Manufacturer.ToList();
        }

        public Manufacturer GetById(long Id)
        {
           return _context.Manufacturer.FirstOrDefault(x => x.ID == Id);
        }

        public Manufacturer GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Manufacturer GetManufacturerForDelete(long Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Manufacturer Manufacturer)
        {
            _context.Manufacturer.Add(Manufacturer);
        }

        public void Update(Manufacturer Manufacturer)
        {
            throw new NotImplementedException();
        }
    }
}
