using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class SearchCampaignCreteriaDto
    {                
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ClientId { get; set; }
        public int? RegionId { get; set; }
        public List<int> BusinessTypes { get; set; }
        public List<int> Towns { get; set; } 
        
    }
}
