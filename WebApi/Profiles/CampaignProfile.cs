using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class CampaignProfile : Profile
    {
        public CampaignProfile()
        {
            // external to internal
            CreateMap<CampaignCreateDto, Campaign>();

            // internal to Externa
            CreateMap<Campaign, CampaignReadDto>();
        }
    }
}
