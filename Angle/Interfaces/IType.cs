using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Models.Models;
using LUNA.Models.Models;


namespace Angle.Interfaces
{
    public interface IType
    {
        List<PType> GetAll();
        PType GetById(long? Id);
        PType GetByName(string name);
        void SetParent(long ParentID, long ChildID);
        void Insert(PType type);
        void Update(PType type);
        void Delete(PType type);
        PType getChilds(long typeId);
   


    }
}
