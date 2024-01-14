using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class TownRepository: Repository<Town>, ITownRepository
    {       
        public TownRepository(MyDataBaseContext dbCtx) : base(dbCtx) { }

        public List<Town> GetFullTowns()
        {
            return Entities.Where(x=> x.Activated == true).Include(x => x.Region).ToList();
        }

        public List<Town> GetTownsInListIds(List<int> idsList)
        {
            var list = Entities.Where(x => idsList.Contains(x.Id) && x.Activated == true).ToList();
            return list;
        }
    }
}
