using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IHistory
    {
        List<History> GetAll();
        History GetById(long Id);
        void Insert(History history);
        void Update(History history);
        void Delete(History history);
        long GetByName(string name);
    }
}
