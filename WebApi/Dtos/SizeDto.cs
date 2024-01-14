using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class SizeDto
    {
        public float Height { get; set; }
        public float Width { get; set; }
        public DimensionUnit Unit { get; set; }       
    }
}
