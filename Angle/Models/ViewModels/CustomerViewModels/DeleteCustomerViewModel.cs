using LUNA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.CustomerViewModels
{
    public class DeleteCustomerViewModel
    {
        public long ID { get; set; }
        public string CompanyName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressBill { get; set; }
        public string PhoneNumber { get; set; }
        public List<Product> products { get; set; }
        public bool decisionControll { get; set; }
    }
}
