using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class BusninessTypeRepository : Repository<BusinessType>, IBusninessTypeRepository
    {
        public BusninessTypeRepository(MyDataBaseContext ctx): base(ctx) { }

        public ICollection<BusinessType> GetActivatedBusinessTypes()
        {            
            return Entities.Where(x => x.Activated == true).ToList();
        }

        public ICollection<BusinessType> GetBusinessInListMapCodes(List<string> businessTypesIds)
        {
            return Entities.Where(x => businessTypesIds.Contains(x.MapCode)).ToList();           
        }
    }
}
