using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(MyDataBaseContext context) : base(context) { }

        public ICollection<Region> GetListActivatedRegions()
        {
            return Entities.Where(x => x.Activated == true).ToList();
        }
    }
}
