using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{

    // BusinessType ligne devis
    public class BusinessTypeQuoteLine: BaseEntity
    {
        public int BusinessTypeId { get; set; }
        public string BusinessTypeCode { get; set; }
        public float BusinessCost { get; set; }
        public int NbrBusinessTypePerCampagne { get; set; }

        [ForeignKey(nameof(Quote))]
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
        
    }
}
