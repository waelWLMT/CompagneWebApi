using BL.ServicePattern;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public interface IRegionService : IServicePattern<Region>
    {
        ICollection<Region> GetListActivatedRegions();
    }
}
