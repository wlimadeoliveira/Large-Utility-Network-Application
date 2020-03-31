using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models
{
    public class ToDoDataContext : DbContext
    {
        public DbSet<ToDoModel> ToDos { get; set; }
        
        public ToDoDataContext(DbContextOptions<ToDoDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }





    }
}
