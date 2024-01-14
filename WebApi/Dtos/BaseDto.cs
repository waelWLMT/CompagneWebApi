
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class BaseDto
    {       
        public int Id { get; set; }  
        [Display(Name ="DateCreation")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0: yyyy-MM-dd}")]       
        public DateTime CreatedAt { get; set; }
    }
}
