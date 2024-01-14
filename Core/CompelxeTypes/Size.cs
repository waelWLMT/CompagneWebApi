using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.CompelxeTypes
{

    [ComplexType]    
    public class Size
    {
        public float Height { get; set; }
        public float Width { get; set; }
        public DimensionUnit Unit { get; set; }
       
    }
}
