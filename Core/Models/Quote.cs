using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    // Devis
    public class Quote: BaseEntity
    {

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string RegionName { get; set; }
        public int TownsNumber { get; set; }       
        public QuoteState State { get; set; }
        public float TotalCost { get; set; }
        public int PenetrationRate { get; set; }
        public int CampaignId { get; set; }
        public int NbrBusinesses { get; set; }
        public float BusinessCost { get; set; }
        public virtual ICollection<ProductQuoteLine> ProductQuoteLines { get; set; }
        public virtual ICollection<BusinessTypeQuoteLine> BusinessTypeQuoteLines { get; set; }

    }
}
