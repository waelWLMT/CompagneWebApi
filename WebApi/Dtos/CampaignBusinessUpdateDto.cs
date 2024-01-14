using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class CampaignBusinessUpdateDto
    {
        public int CampaignId { get; set; }
        public int BusinessCampaignId { get; set; }
        public int NewStateId { get; set; }
        public int UserModifId { get; set; }
        
    }
}
