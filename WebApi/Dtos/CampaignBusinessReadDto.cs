using Core.Enums;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class CampaignBusinessReadDto
    {        
        public int CampaignBusinessId { get; set; }        
        public int CompagnId { get; set; }        
        public int BusinessTypeId { get; set; }
        public BusinessState State { get; set; }
        public int BusinessTownId { get; set; }
        public Place Place { get; set; }
    }
}
