using BL.ServicePattern;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public interface ITownService : IServicePattern<Town>
    {
        List<Town> GetFullTowns();
        List<Town> GetTownsByRegion(int regionId, bool fullEntity);

        List<Town> GetTownsByIdsList(List<int> idsList);
    }
}
