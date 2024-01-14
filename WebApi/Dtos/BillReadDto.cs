using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class BillReadDto: BaseDto
    {
        public int QuoteId { get; set; }
        public int CampaignId { get; set; }
        public float FinalTotalCost { get; set; }
        public int NbrTowns { get; set; }
        public string RegionName { get; set; }
        public CustomerReadDto Customer { get; set; }
        public virtual ICollection<BillProductReadDto> BillProducts { get; set; }
        public virtual ICollection<BillBusinessReadDto> BillBusinesses { get; set; }
    }
}
