using Angle.Interfaces;
using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories
{
    public class CustomerRepo : ICustomer
    {
        private readonly ProjectDataContext _context;

        public CustomerRepo(ProjectDataContext context)
        {
            _context = context;
        }


        public void Delete(Customer customer)
        {
            _context.Customer.Remove(customer);
        }

        public List<Customer> GetAll()
        {
            return _context.Customer.ToList();
        }

        public Customer GetById(long Id)
        {
            return _context.Customer.FirstOrDefault(x => x.ID == Id);
        }

        public Customer GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer customer)
        {
            _context.Customer.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Customer.Update(customer);
        }
    }
}
