using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LUNA.Models
{
    public class AccountViewModel
    {
        /*  [Required]
          [EmailAddress, MaxLength(500)]
          public string EmailAddress { get; set; }
          [Required]
          [DataType(DataType.Password)]
          public string Password { get; set; }
          [Compare("Password", ErrorMessage = "Passwords must match")]
          public string ConfirmPassword { get; set; }
          public bool RememberMe { get; set; }*/
        public RegisterViewModel RegisterModel { get; set; }
        public LoginViewModel LoginModel { get; set; }


    }
}
