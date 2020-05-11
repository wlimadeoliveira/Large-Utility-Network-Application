using Angle.Models.Models;
using LUNA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IIndex_QR
    {
        List<Index_QR> GetAll();
        Index_QR GetById(long Id);
        void Insert(Index_QR Index_QR);
        void Update(Index_QR Index_QR);
        void Delete(Index_QR Index_QR);
    }
}
