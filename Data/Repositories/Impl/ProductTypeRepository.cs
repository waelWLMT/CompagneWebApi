using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository, IRepository<ProductType>
    {
        public ProductTypeRepository(MyDataBaseContext context) : base(context) { }

        public ICollection<ProductType> GetproductTypeInListIds(List<int> productTypeIds)
        {
            var list = Entities.Where(x => productTypeIds.Contains(x.Id)).ToList();
            return list;
        }
    }
}
