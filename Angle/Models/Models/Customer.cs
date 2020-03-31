using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models
{
    public class Customer
    {  [Key]
        public long ID { get; set; }

       
        public string CompanyName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressBill { get; set; }
        public string PhoneNumber { get; set; }

        // history für customer wäre auch gut. wenn man z.B. telefoniert hat/kontakt hatte

        /*[ForeignKey("CustomerID")]
        public ICollection<ProjectModel> Projects;*/
        //public IList<PersonModel> Persons;
       // public IList<HistoryModel> Histories;


    }
}
