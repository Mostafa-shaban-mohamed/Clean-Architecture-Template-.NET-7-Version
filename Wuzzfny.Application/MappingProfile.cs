using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wuzzfny.Application.Users.Dtos;
using Wuzzfny.Domain.Common;
using Wuzzfny.Domain.Models.Users;

namespace Wuzzfny.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            //add mapping of DTo to the model or vice versa
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<EntityList<User>, ReturnListDto<UserDto>>().ReverseMap();
        }
    }
}
