using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class TownProfile : Profile
    {
        public TownProfile()
        {
            // extern to intern 
            CreateMap<TownReadDto, Town>();

            // intern to extern
            CreateMap<Town, TownReadDto>();
        }
    }
}
