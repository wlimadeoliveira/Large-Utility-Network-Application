using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models.Models;
using Angle.Models.Models.Ivy;
namespace Angle.Interfaces
{
    public interface ILocation
    {
        List<Location> GetAll();
        Location GetById(long Id);
        Location GetByName(string name);
        void Insert(Location Location);
        void Update(Location Location);
        void Delete(Location Location);
        Location GetLocationForDelete(long Id);
    }
}
