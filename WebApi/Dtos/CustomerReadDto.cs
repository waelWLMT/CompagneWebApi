using Core.CompelxeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class CustomerReadDto : BaseDto
    {
        public string Name { get; set; }
        public string TaxIdNumber { get; set; }
        public Address Address { get; set; }
        public string Mail { get; set; }
        public string TelNumber { get; set; }

    }
}
