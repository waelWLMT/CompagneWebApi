using Core.CompelxeTypes;
using Core.Models;
using Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class MyDataBaseContext : DbContext
    {

        #region test dbSets
        // just for test
        //public DbSet<Author> Authors { get; set; }
        //public DbSet<Book> Books { get; set; }
        #endregion

        // real object for compagn Pub App 
        public DbSet<BillProduct> BillProducts { get; set; }
        public DbSet<BillBusiness> BillBusinesses { get; set; } 
        public DbSet<Bill> Bills { get; set; }  
        public DbSet<BusinessTypeQuoteLine> BusinessTypeQuoteLines { get; set; }
        public DbSet<ProductQuoteLine> productQuoteLines { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }        
        public DbSet<RoleMenu> RoleMenus { get; set; }        
        public DbSet<User> Users { get; set; }      
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<ProductType> ProductTypes { get; set; }  
        public DbSet<Town> Towns { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<BusinessType> businessTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
              
        // campaignBusnisses
        public DbSet<CampaignBusiness> CompaignBusinesses { get; set; }
               
        // CampaignProducts
        public DbSet<Product> Products { get; set; }

        public MyDataBaseContext(DbContextOptions<MyDataBaseContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {      
            // Menu Role Configurations
             modelBuilder.ApplyConfiguration(new RoleMenuConfigurations());

            // Campaign Configurations
            modelBuilder.ApplyConfiguration(new CampaignConfigurations());
                        
            // Campaign Business Configurations
            modelBuilder.ApplyConfiguration(new CampaignBusinessConfigurations());            

            // Campaign Product Configurations
            modelBuilder.ApplyConfiguration(new CampaignProductsConfiguration());

            // Product types Configurations
            modelBuilder.ApplyConfiguration(new ProductTypeConfigurations());

            // Customer Configurations
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            
        }
    }
}
