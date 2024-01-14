using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IRegionRepository : IRepository<Region>
    {

        ICollection<Region> GetListActivatedRegions();
    }
}
