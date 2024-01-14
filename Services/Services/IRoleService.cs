using BL.ServicePattern;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public interface IRoleService: IServicePattern<Role>
    {
        public Role GetRoleWithMenusById(int roleId);
        public HashSet<Menu> GetMenusByRoleId(int roleId);
    }
}
