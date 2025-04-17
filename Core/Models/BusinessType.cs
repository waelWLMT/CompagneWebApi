using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class BusinessType : BaseEntity
    {
        // properties
        /**
         * ceci est la liste des propriété de type de lieu 
         * exemple : TagKeyCode: "shop", TagValueCode: "bakery", TagKeyDesignation: "Commerce", TagValueDesignation: "Boulangerie"        
        */
        public string TagKeyCode { get; set; }
        public string TagValueCode { get; set; }
        public string TagKeyDesignation { get; set; }
        public string TagValueDesignation { get; set; }
        public bool Activated { get; set; }

        // navigation properties
        public virtual ICollection<Campaign> Campaigns { get; set; }

    }
}
