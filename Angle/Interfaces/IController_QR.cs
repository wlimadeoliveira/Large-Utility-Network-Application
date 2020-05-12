using Angle.Models.Models;
using LUNA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IController_QR
    {
        List<Controller_QR> GetAll();
        Controller_QR GetById(long id);
        void Insert(Controller_QR controller_QR);
        void Update(Controller_QR controller_QR);
        void Delete(Controller_QR controller_QR);
    }
}
