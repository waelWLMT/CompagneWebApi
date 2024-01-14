using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ProductTypeReadDto : BaseDto
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public SizeDto Size { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int DefaultNbrProductPerBusiness { get; set; }
    }
}
