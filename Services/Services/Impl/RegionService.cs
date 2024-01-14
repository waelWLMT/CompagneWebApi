using BL.ServicePattern;
using Core.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Services.Impl
{
    public class RegionService : ServicePattern<Region>, IRegionService
    {
        private readonly IRegionRepository _regionRepo;

        public RegionService(IRegionRepository regionRepo) : base(regionRepo)
        {
            _regionRepo = regionRepo;
        }

        public ICollection<Region> GetListActivatedRegions()
        {
            return _regionRepo.GetListActivatedRegions();
        }
    }
}
