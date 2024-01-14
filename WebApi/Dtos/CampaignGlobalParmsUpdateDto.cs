using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{

    // à completer plus tard
    public class CampaignGlobalParmsUpdateDto
    {       
        public string Title { get; set; }
        public string Goal { get; set; }
        public float ForecastBudget { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string Description { get; set; }
        public int PenetrationRate { get; set; }
    }
}
