using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class Campaign : BaseEntity
    {
        #region properties
        public string Title { get; set; }
        public string Goal { get; set; }
        public float ForecastBudget { get; set; }
        public int PenetraionRate { get; set; }
        public string Description { get; set; }
        public float TotalCost { get; set; }
        public DateTime ExecutionDate { get; set; }
        public DateTime CloseDate { get; set; }
        public CampaignState CampaignState { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        // foreign keys 
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }

        public int LastUserModifId { get; set; }

        #endregion

        #region navigation properties 

        public User User { get; set; }
        public Customer Customer { get; set; }
        public Region Region { get; set; }
        public virtual ICollection<Town> CampaignTowns { get; set; }        
        public virtual ICollection<BusinessType> CampaignBusinessTypes { get; set; }
        public virtual ICollection<CampaignBusiness> CampaignBusinesses { get; set; }        
        public virtual ICollection<Product> CampaignProducts { get; set; }
        

        #endregion

    }
}
