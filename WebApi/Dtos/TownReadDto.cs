using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class TownReadDto : BaseDto
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Departement { get; set; }
        public string Canton { get; set; }
        public string Borough { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public string TownWording { get; set; }
        public int RegionId { get; set; }
    }
}
