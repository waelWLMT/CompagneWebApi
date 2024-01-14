using BL.ServicePattern;
using BL.Services;
using BL.Services.Impl;
using Data.Repositories;
using Data.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Root
{
    public class CompositionRoot
    {
        public CompositionRoot() { }

        public static void InjectDependencies(IServiceCollection services,  string connectionString)
        {
            #region // databases injection
            services.AddDbContext<MyDataBaseContext>(opts => opts.UseSqlServer(connectionString, b => b.MigrationsAssembly("Data")));
            services.AddScoped<MyDataBaseContext>();
            #endregion

            #region // generic repo and services injection
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IServicePattern<>), typeof(ServicePattern<>));
            #endregion

            #region // repositories injection           
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<ITownRepository, TownRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IBusninessTypeRepository, BusninessTypeRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();

            services.AddScoped<IBillRepository, BillRepository>();

            // the implementation of IPlacesRepositoy is gonna be replaced by Places Repository
            services.AddScoped<IPlacesRepository, MockPlacesRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            
            #endregion

            #region // services injection
           
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<ITownService, TownService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IBusninessTypeService, BusninessTypeService>();            
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IQuoteService, QuoteService>();

            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IFilesService, FilesService>();

            #endregion

            //services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
