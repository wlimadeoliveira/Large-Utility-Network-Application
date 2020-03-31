using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models
{
    public class CustomerViewModel
    {

        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress, MaxLength(500)]
        public string Email { get; set; }
        [Phone, MaxLength(50)]
        public string Phone { get; set; }
        public string Company { get; set; }


    }
}
