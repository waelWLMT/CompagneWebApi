using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class RoleReadDto: BaseDto
    {

        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Activated { get; set; }
    }
}
