using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IPerson
    {
        List<Person> GetAll();
        Person GetById(long Id);
        Person GetByName(string name);
        void Insert(Person person);
        void Update(Person person);
        void Delete(Person person);
    }
}
