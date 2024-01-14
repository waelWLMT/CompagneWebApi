using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class CampaignBusiness
    {

        [Key, Column(Order = 1)]
        public int CampaignBusinessId { get; set; }
        
        // properties && foreign keys
        [ForeignKey(nameof(Campaign))]        
        public int CompagnId { get; set; }

        [ForeignKey(nameof(BusinessType))]        
        public int BusinessTypeId { get; set; }
        public BusinessState State { get; set; }
        public int BusinessTownId { get; set; }
        public Place Place { get; set; }
        public int UserModifId { get; set; }
        public DateTime LastDateModif { get; set; }

        // navigation properties
        public BusinessType BusinessType { get; set; }
        public Campaign Campaign { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }       

    }
}
