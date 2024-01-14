using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ProductQuoteLineReadDto: BaseDto
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int NbrProductPerBusiness { get; set; }
        public float UnitPrice { get; set; }
        public float CostPerBusiness { get; set; }       
        public int QuoteId { get; set; }
      
    }
}
