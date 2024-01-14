using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ProductUpdateDto
    {
        public int ProductTypeId { get; set; }
        public int FinalNbrProductPerBusiness { get; set; }
        public float FinalPrice { get; set; }
    }
        
}
