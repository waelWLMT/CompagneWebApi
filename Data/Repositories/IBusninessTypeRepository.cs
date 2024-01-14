using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IBusninessTypeRepository : IRepository<BusinessType>
    {

        ICollection<BusinessType> GetActivatedBusinessTypes();
        ICollection<BusinessType> GetBusinessInListMapCodes(List<string> businessTypesIds);

// test
    }
}
