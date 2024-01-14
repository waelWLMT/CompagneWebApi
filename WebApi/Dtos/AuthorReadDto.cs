using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class AuthorReadDto : BaseDto
    {        
        public String Name { get; set; }
        public String Country { get; set; }
    }
}
