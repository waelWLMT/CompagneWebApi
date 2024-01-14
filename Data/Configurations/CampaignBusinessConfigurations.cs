using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Configurations
{
    class CampaignBusinessConfigurations : IEntityTypeConfiguration<CampaignBusiness>
    {        
        public void Configure(EntityTypeBuilder<CampaignBusiness> builder)
        {
            //builder.HasKey(cb => new { cb.BusinessTypeId, cb.CompagnId, cb.BusinessTownId });
            builder.OwnsOne(x => x.Place);
        }
    }
}
