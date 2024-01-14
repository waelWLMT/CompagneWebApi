using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(MyDataBaseContext context): base(context) { }

        public Role GetRoleUserWithMenus(int roleId)
        {
            var role = base.Entities
                .Where(x => x.Id == roleId)
                .Include(x => x.RoleMenus)
                .ThenInclude(x => x.Menu)
                .FirstOrDefault();

            return  role;
        }
    }
}
