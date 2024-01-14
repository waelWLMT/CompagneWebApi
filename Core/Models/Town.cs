using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{    
    public class Town : BaseEntity
    {  

        // Properties
        public string City { get; set; }
        public string PostalCode { get; set; }        
        public string Departement { get; set; }
        public string Canton { get; set; }
        public string Borough { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public string TownWording { get; set; }
        public bool Activated { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }

        // Navigation properties
        public virtual Region Region { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}