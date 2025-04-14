using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class BusinessType : BaseEntity
    {        
        // properties
        public string MapCode { get; set; }
        public string Designation { get; set; }
        public string GeoApiPlaceCode { get; set; }
        public string GeoApiPlaceCategory { get; set; }
        public string Description { get; set; }
        public bool Activated { get; set; }

        // navigation properties
        public virtual ICollection<Campaign> Campaigns { get; set; }

    }
}
