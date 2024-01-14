using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class BillProduct: BaseEntity
    {        
        public string ProductTypeName { get; set; }
        public float FinalUnitPrice { get; set; }
        public int NbrProductPerBusiness { get; set; }
        public float CostPerBusiness { get; set; }
        public Bill Bill { get; set; }
    }
}
