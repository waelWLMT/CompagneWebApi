using AutoMapper;
using Core.CompelxeTypes;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<SizeDto, Size>();

            // ExterneModel (dto) => InternModel(Entity)
            CreateMap<ProductTypeCreateDto, ProductType>();
            CreateMap<ProductTypeReadDto, ProductType>();

            // InternalModel => ExterneModel
            CreateMap<ProductType, ProductTypeReadDto>();
            CreateMap<Size, SizeDto>();
        }
    }
}
