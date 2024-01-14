using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class DetailsCampaignTown
    {
        public Town Town { get; set; }
        public List<CampaignBusiness> TownBusinesses { get; set; }
        public int NbrBusinesses { get; set; }
        public double TownCost { get; set; }
        public int PenetrationRate { get; set; }
                     
    }
}
