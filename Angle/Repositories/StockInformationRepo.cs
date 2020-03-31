using Angle.Interfaces;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories
{
    public class StockInformationRepo : IStockInformation
    {

        private readonly ProjectDataContext _context;
        public StockInformationRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void Delete(StockInformation stockInformation)
        {
            _context.StockInformation.Remove(stockInformation);
        }

        public List<StockInformation> GetAll()
        {
            return _context.StockInformation.ToList(); 
        }

        public StockInformation GetById(long Id)
        {
            return _context.StockInformation.FirstOrDefault(x => x.ID == Id);
        }

        public StockInformation GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(StockInformation stockInformation)
        {
            _context.StockInformation.Add(stockInformation);
        }

        public void Update(StockInformation stockInformation)
        {
            _context.StockInformation.Update(stockInformation);
        }
    }
}
