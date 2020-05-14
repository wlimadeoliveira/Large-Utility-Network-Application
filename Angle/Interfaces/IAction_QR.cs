using Angle.Models.Models;
using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IAction_QR
    {
        List<Action_QR> GetAll();
        Action_QR GetById(long id);
        void Insert(Action_QR action_QR);
        void Update(Action_QR action_QR);
        void Delete(Action_QR action_QR);
    }
}
