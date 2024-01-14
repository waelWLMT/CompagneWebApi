using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Activated { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
        
}
