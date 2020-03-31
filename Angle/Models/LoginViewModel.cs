using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress, MaxLength(50)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public  bool RememberMe { get; set; }




    }
}
