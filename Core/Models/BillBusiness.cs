using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class BillBusiness: BaseEntity
    {
        public string BusinessName { get; set; }
        public string BusinessTypeName  { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string TownName { get; set; }
        public float BusinessCost { get; set; }
        public Bill Bill { get; set; }
    }
}
