using Core.CompelxeTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public Size Size { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int DefaultNbrProductPerBusiness { get; set; }        
    }
}
