using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.CompelxeTypes
{    
    public class Address
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string TownName { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
    }
}
