using Angle.Models.ViewModels.AttributeViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models.Models;
using Attribute = LUNA.Models.Models.Attribute;
using Angle.Models.ViewModels.TypeViewModels;

namespace Angle.Helpers
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<Attribute, AttributeViewModel>().ReverseMap();
            CreateMap<CreateAttributeViewModel, Attribute>();
            CreateMap<PType, TypeViewModel>().ReverseMap();
            CreateMap<TypeCreateViewModel, PType>();
        }
    }
}
