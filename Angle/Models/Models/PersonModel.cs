using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models.Models
{
    public class Person
    {
        [Key]
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Language { get; set; }
        public string CellPhoneNumber { get; set; }
        public string InternNumber { get; set; }
        public string Email { get; set; }
        public long? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer {get;set;}
        public List<ProjectPerson> ProjectPersons { get; set; }
       

    }
}
