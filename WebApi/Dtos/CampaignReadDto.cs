using AutoMapper;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class CampaignReadDto : BaseDto
    {
        #region properties
        public string Title { get; set; }
        public string Goal { get; set; }
        public float ForecastBudget { get; set; }
        public int PenetraionRate { get; set; }
        public string Description { get; set; }
        public float TotalCost { get; set; }
        public DateTime ExecutionDate { get; set; }
        public CampaignState CampaignState { get; set; }         
        public int CustomerId { get; set; }       
        public int RegionId { get; set; }

        #endregion

        #region navigation properties 
        public CustomerReadDto Customer { get; set; }
        public RegionReadDto Region { get; set; }
        public  ICollection<TownReadDto> CampaignTowns { get; set; }        
        public  ICollection<BusinessTypeReadDto> CampaignBusinessTypes { get; set; }
        public  ICollection<CampaignBusinessReadDto> CampaignBusinesses { get; set; }
        public  ICollection<ProductReadDto> CampaignProducts { get; set; }

        #endregion

    }
}
