using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IStockInformation
    {
        List<StockInformation> GetAll();
        StockInformation GetById(long Id);
        StockInformation GetByName(string name);
        void Insert(StockInformation stockInformation);
        void Update(StockInformation stockInformation);
        void Delete(StockInformation stockInformation);
    }
}
