using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Builders
{
    public static class CampaignBuilders
    {
        public static Campaign BuildCompaign(CampaignCreateDto campaignCreateDto)
        {
            var campaign = new Campaign();
            
            campaign.Title = campaignCreateDto.Title;
            campaign.Goal = campaignCreateDto.Goal;
            campaign.CustomerId = campaignCreateDto.CustomerId;
            campaign.ForecastBudget = campaignCreateDto.ForecastBudget;
            campaign.RegionId = campaignCreateDto.RegionId;
            campaign.CampaignTowns = new HashSet<Town>();
            campaign.CampaignBusinesses = new HashSet<CampaignBusiness>();            
            campaign.ExecutionDate = campaignCreateDto.ExecutionDate;
            campaign.Description = campaignCreateDto.Description;

            return campaign;
        }
    }
}
