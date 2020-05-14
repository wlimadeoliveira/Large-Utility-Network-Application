using Angle.Models.Models;
using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IIndex_QR
    {
        List<Index_QR> GetAll();
        Index_QR GetByProductId(long id);
        void Insert(Index_QR index_QR);
        void Update(Index_QR index_QR);
        void Delete(Index_QR index_QR);
    }
}
