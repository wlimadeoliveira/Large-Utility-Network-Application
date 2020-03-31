using Angle.Interfaces;
using LUNA.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories
{
    public class PersonRepo : IPerson
    {
        private readonly ProjectDataContext _context;
       
        public PersonRepo(ProjectDataContext context)
        {
            _context = context;
         
        }

        public void Delete(Person person)
        {
            _context.Persons.Remove(person);
        }

        public List<Person> GetAll()
        {
            return _context.Persons.Include(x => x.Customer).ToList();
        }

        public Person GetById(long Id)
        {
            return _context.Persons.Include(y => y.Customer).FirstOrDefault(x => x.ID == Id);
        }

        public Person GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Person person)
        {
            _context.Persons.Add(person);
        }

        public void Update(Person person)
        {
            _context.Persons.Update(person);
        }
    }
}
