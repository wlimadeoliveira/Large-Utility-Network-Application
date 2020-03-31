using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models.Models;
using Angle.Models.Models.Ivy;
namespace Angle.Interfaces
{
    public interface IManufacturer
    {
        List<Manufacturer> GetAll();
        Manufacturer GetById(long Id);
        Manufacturer GetByName(string name);
        void Insert(Manufacturer Manufacturer);
        void Update(Manufacturer Manufacturer);
        void Delete(Manufacturer Manufacturer);
        Manufacturer GetManufacturerForDelete(long Id);
    }
}
