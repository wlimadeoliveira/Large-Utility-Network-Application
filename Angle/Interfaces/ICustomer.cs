using LUNA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface ICustomer
    {
        List<Customer> GetAll();
        Customer GetById(long Id);
        Customer GetByName(string name);
        void Insert(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
