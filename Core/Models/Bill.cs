using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class Bill: BaseEntity
    {
        public int QuoteId { get; set; }
        public int CampaignId { get; set; }
        public float FinalTotalCost { get; set; }
        public int NbrTowns { get; set; }
        public string RegionName { get; set; }
        public Customer Customer { get; set; }       
        public virtual ICollection<BillBusiness> BillBusinesses { get; set; }
        public virtual ICollection<BillProduct> BillProducts { get; set; }

    }
}
