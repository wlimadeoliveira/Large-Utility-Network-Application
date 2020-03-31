using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models.Models;
using Angle.Models.Models.Ivy;
namespace Angle.Interfaces
{
    public interface ISupplier
    {
        List<Supplier> GetAll();
        Supplier GetById(long Id);
        Supplier GetByName(string name);
        void Insert(Supplier Supplier);
        void Update(Supplier Supplier);
        void Delete(Supplier Supplier);
        Supplier GetSupplierForDelete(long Id);
    }
}
