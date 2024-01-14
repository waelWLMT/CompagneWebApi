using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class BillBusinessReadDto
    {
        public string BusinessName { get; set; }
        public string BusinessTypeName { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string TownName { get; set; }
        public float BusinessCost { get; set; }        
    }
}
