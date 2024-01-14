using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Region : BaseEntity
    {        

        // properties
        public string Name { get; set; }
        public bool Activated { get; set; }

        // navigation properties
        public virtual ICollection<Town> RegionTowns { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }

    }
}
