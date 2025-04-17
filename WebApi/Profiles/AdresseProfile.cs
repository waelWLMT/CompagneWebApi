using AutoMapper;
using Core.CompelxeTypes;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class AdresseProfile : Profile
    {
        public AdresseProfile()
        {
            CreateMap<AddressReadDto, Address>();
        }
    }
}
