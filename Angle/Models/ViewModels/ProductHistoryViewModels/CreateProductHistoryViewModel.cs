using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.ProductHistoryViewModels
{
    public class CreateProductHistoryViewModel
    {
        public long HistoryID { get; set; }
        public long ProductID { get; set; }
        public string Email { get; set; }
        public DateTime DateTimeNow { get; set; }
        public string Comment { get; set; }
        public long OldHistoryID { get; set; }
        public long UploadID { get; set; }
        public IFormFile UploadFile { get; set; }
    }
}
