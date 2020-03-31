using Angle.Interfaces.Ivy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IUnityOfWork
    {
        IProduct Product { get; }
        IAttribute Attribute { get; }
        IType Type { get; }
        IFeature Feature { get;  }
        ICustomer Customer { get; }
        IPerson Person { get; }
        IProject Project { get; }
        IStockInformation StockInformation { get; }
        IManufacturer Manufacturer { get; }
        ISupplier Supplier { get; }
        ICategory Category { get; }
        ILocation Location { get; }
        IArticle Article { get; }
        IHistory History { get; }
        void Save();   
    }
}
