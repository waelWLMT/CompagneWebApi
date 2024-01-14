using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            // ExterneModel (dto) => InternModel(Entity)
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerReadDto, Customer>();

            // InternalModel => ExterneModel
            CreateMap<Customer, CustomerReadDto>();
        }
        
    }
}
