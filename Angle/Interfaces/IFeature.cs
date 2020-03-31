using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IFeature
    {
        List<Feature> GetAll();
        Feature GetById(long Id);
        Feature GetByName(string name);
        Feature GetFeatureForDeleteById(long id);
        void Insert(Feature feature);
        void Update(Feature feature);
        void Delete(Feature feature);
    }
}
