
using LUNA.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.ViewModels.TypeViewModels
{
    public class TypeCreateViewModel
    {
        public long ID { get; set; }
        [Remote(action: "NameExist", controller: "Type")]
        public string Name { get; set; }
        public string Description { get; set; }
        public long[] SelectedFeatures { get; set; }
        public string[] SelectedAttributes { get; set; }
        public string[] ValueSelectedAttributes { get; set; }
        public long[] TypeChild { get; set; }
        public long [] Quantity { get; set; }

    }
}
