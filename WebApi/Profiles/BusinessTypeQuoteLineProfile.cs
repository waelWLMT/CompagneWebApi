using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class BusinessTypeQuoteLineProfile: Profile
    {
        public BusinessTypeQuoteLineProfile()
        {
            // external to internal model
            CreateMap<BusinessTypeQuoteLineReadDto, BusinessTypeQuoteLine>();

            // internal to external model
            CreateMap<BusinessTypeQuoteLine, BusinessTypeQuoteLineReadDto>();
        }
    }
}
