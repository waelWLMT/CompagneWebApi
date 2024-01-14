using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class BusinessPhotoReadDto: BaseDto
    {       
        public string ImageName { get; set; }
        public string FileFolder { get; set; }        
    }
}
