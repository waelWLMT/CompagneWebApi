using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // ExterneModel (dto) => InternModel(Entity)
            CreateMap<UserCreateDto, User>();
            CreateMap<UserReadDto, User>();

            // InternModel(Entity)  => ExterneModel (dto)
            CreateMap<User, UserReadDto>();
            CreateMap<User, AuthenticatedUserDto>();
        }
    }
}
