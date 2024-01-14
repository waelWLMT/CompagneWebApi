using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class DetailsCampaignTownReadDto
    {
        public TownReadDto Town { get; set; }
        public List<CampaignBusinessReadDto> TownBusinesses { get; set; }
        public int NbrBusinesses { get; set; }
        public double TownCost { get; set; }

        public int PenetrationRate { get; set; }

    }
}
