using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models
{
    public class CustomerDataContext : DbContext
    {
        public DbSet<CustomerViewModel> Customers { get; set; }

        public CustomerDataContext (DbContextOptions<CustomerDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }




    }
}
