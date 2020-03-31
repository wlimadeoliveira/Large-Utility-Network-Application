using Angle.Models.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.AccountViewModel
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public List<ProductHistory> ProductHistories { get; set; }
        public List<QuickAdventure> QuickAdventures { get; set; }
    }
}
