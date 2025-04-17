using AutoMapper;
using Core.Models;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<PlaceReadDto, Place>();
        }
    }
}
