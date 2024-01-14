using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class ProductQuoteLineProfile : Profile
    {
        public ProductQuoteLineProfile()
        {
            // internal to external model
            CreateMap<ProductQuoteLineReadDto, ProductQuoteLine>();

            // external to internal model
            CreateMap<ProductQuoteLine, ProductQuoteLineReadDto>();
        }
    }
}
