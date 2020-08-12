using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.ProductViewModel
{
    public class FileUploadViewModel
    {
        [FromForm(Name = "File")]
        public IFormFile File { get; set; }
        [FromForm(Name = "ProductID")]
        public long ProductID { get; set; }
        [FromForm(Name = "Description")]
        public string Description { get; set; }
        [FromForm(Name = "Email")]
        public string Email { get; set; }
    }
}
