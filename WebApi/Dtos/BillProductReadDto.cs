using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class BillProductReadDto
    {
        public string ProductTypeName { get; set; }
        public float FinalUnitPrice { get; set; }
        public int NbrProductPerBusiness { get; set; }
        public float CostPerBusiness { get; set; }       
    }
}
