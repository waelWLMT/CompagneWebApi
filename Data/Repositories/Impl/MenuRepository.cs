using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Impl
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(MyDataBaseContext context): base(context) { }
    }
}
