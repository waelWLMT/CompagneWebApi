using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{

    // produit LigneProduit
    public class ProductQuoteLine : BaseEntity
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int NbrProductPerBusiness { get; set; }
        public float UnitPrice { get; set; }
        public float CostPerBusiness { get; set; }

        [ForeignKey(nameof(Quote))]
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}
