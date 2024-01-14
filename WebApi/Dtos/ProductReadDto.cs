using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ProductReadDto
    {
        public int ProductTypeId { get; set; }
        public int CampaignId { get; set; }
        public int NbrProductPerBusiness { get; set; }
        public double FinalUnitPrice { get; set; }
        public ProductTypeReadDto ProductType { get; set; }
    }
}
