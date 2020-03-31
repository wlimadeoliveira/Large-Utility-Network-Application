using Angle.Interfaces;
using Angle.Models.Models.Ivy;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories.Ivy
{
    public class LocationRepo : ILocation
    {

        private readonly ProjectDataContext _context;
        public LocationRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void Delete(Location Location)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetAll()
        {
            return _context.Location.ToList();
        }

        public Location GetById(long Id)
        {
           return _context.Location.FirstOrDefault(x => x.ID == Id);
        }

        public Location GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Location GetLocationForDelete(long Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Location Location)
        {
            _context.Location.Add(Location);
        }

        public void Update(Location Location)
        {
            throw new NotImplementedException();
        }
    }
}
