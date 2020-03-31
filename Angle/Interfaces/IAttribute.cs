using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models.Models;
using Attribute = LUNA.Models.Models.Attribute;

namespace Angle.Interfaces
{
    public interface IAttribute
    {
        List<Attribute> GetAll();
        Attribute GetById(long Id);
        Attribute GetByName(string name);
        void Insert(Attribute attribute);
        void Update(Attribute attribute);
        void Delete(Attribute attribute);
        Attribute GetAttributeForDelete(long Id);
    }
}
