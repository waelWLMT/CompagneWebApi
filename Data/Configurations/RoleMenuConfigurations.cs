using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Configurations
{
    public class RoleMenuConfigurations : IEntityTypeConfiguration<RoleMenu>
    {       
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.HasKey(x => new { x.MenuId, x.RoleId });
        }
    }
}
