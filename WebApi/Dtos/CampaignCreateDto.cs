using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class CampaignCreateDto
    {
        public string Title { get; set; }
        public string Goal { get; set; }
        public int CustomerId { get; set; }
        public float ForecastBudget { get; set; }
        public int UserId { get; set; }
        public int RegionId { get; set; }
        public List<int> TownsIds { get; set; }
        public List<string> BusinessTypesIds { get; set; }
        public List<int> ProductTypeIds { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string Description { get; set; }

    }
}
