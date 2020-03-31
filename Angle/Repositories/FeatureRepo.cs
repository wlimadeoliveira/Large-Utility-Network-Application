using Angle.Interfaces;
using LUNA.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories
{
    public class FeatureRepo : IFeature
    {
        private readonly ProjectDataContext _context;
        public FeatureRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void Delete(Feature feature)
        {
            _context.Features.Remove(feature);
        }

        public List<Feature> GetAll()
        {
            return _context.Features.ToList();
        }

        public Feature GetById(long Id)
        {
            return _context.Features.FirstOrDefault(x => x.ID == Id);
        }

        public Feature GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Feature feature)
        {
            _context.Features.Add(feature);
        }

        public void Update(Feature feature)
        {
            _context.Features.Update(feature);
        }


        public Feature GetFeatureForDeleteById(long id)
        {
            return _context.Features.Include(x => x.TypeFeature).FirstOrDefault(y => y.ID == id);
        }

    }
}
