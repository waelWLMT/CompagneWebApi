using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class BusinessTypeQuoteLineReadDto: BaseDto
    {
        public int BusinessTypeId { get; set; }
        public string BusinessTypeCode { get; set; }
        public float BusinessCost { get; set; }
        public int NbrBusinessTypePerCampagne { get; set; }

    }
}
