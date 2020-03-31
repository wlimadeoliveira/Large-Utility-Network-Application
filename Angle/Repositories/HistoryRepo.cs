using Angle.Interfaces;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LUNA.Models;

namespace Angle.Repositories
{
    public class HistoryRepo : IHistory
    {
        private readonly ProjectDataContext _context;
        public HistoryRepo(ProjectDataContext context)
        {
            _context = context;
        }
        public void Delete(History History)
        {
            _context.History.Remove(History);
        }
        public List<History> GetAll()
        {
            return _context.History.ToList();
        }

        public History GetById(long Id)
        {
            return _context.History.FirstOrDefault(x => x.ID == Id);
        }
        public void Insert(History history)
        {
            _context.History.Add(history);
        }
        public long GetByName(string name)
        {
            var history = _context.History.FirstOrDefault(x => x.Description == "Created");
            return history.ID;
        }
        public void Update(History History)
        {
            _context.History.Update(History);
        }
    }
}
