using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            // extern to intern
            CreateMap<RegionReadDto, Region>();
            // intern to extern
            CreateMap<Region, RegionReadDto>();
        }
    }
}
