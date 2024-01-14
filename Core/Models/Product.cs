using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{       
    public class Product 
    {
        // properties
        [ForeignKey(nameof(ProductType))]
        public int ProductTypeId { get; set; }

        [ForeignKey(nameof(Campaign))]
        public int CampaignId { get; set; }
        public int NbrProductPerBusiness { get; set; }
        public double FinalUnitPrice { get; set; }

        // navigation properties
        public ProductType ProductType { get; set; }
        public Campaign Campaign { get; set; }
    }
}
