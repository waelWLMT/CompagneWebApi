using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class RoleMenu
    {        
        [ForeignKey(nameof(Menu))]
        public int MenuId { get; set; }
        
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public bool Activated { get; set; }        
        public Menu Menu { get; set; }
        public Role Role { get; set; }

        
    }
}
