using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Photo : BaseEntity
    {
        // navigation 
        public string ImageName { get; set; }      
        public string FileFolder { get; set; }

        // navigation properties
        public CampaignBusiness CampaignBusiness { get; set; }
    }
}
