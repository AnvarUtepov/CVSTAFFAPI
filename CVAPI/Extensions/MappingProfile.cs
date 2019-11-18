using AutoMapper;
using CVCore.Entities;
using CVCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVAPI.Extensions
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUser, ApplicationUser>();
                  //.ForMember(d=>d.UserName,m=>m.MapFrom(src=>src.Name));
        }
    }
}
