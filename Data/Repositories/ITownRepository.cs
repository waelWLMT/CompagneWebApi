using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ITownRepository : IRepository<Town>
    {
        List<Town> GetTownsInListIds(List<int> idsList);
        List<Town> GetFullTowns();
    }
}
