using BL.ServicePattern;
using Core.Models;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services.Impl
{
    public class TownService : ServicePattern<Town>, ITownService
    {
        private readonly ITownRepository _townRepo;
        public TownService(ITownRepository townRepo) : base(townRepo) {
            _townRepo = townRepo;
        }

        // get Towns with their region
        public List<Town> GetFullTowns()
        {
            var list = _townRepo.GetFullTowns();
            return list;
        }

        public List<Town> GetTownsByIdsList(List<int> idsList)
        {
            return _townRepo.GetTownsInListIds(idsList);
        }

        public List<Town> GetTownsByRegion(int regionId, bool fullEntity)
        {
            var list = fullEntity ? GetFullTowns() : GetAll().Where(x=> x.Activated == true).ToList();

            if (list != null)
                return list.Where(x => x.RegionId == regionId).ToList();

            return new List<Town>();
        }
    }
}
